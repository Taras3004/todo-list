using Microsoft.AspNetCore.Mvc;
using WebApi.Model.Entities.TodoDb;
using WebApp.Models;
using WebApp.Models.ApiClients.TodoListApiClient;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers;

public class TodoTaskController : Controller
{
    private readonly TodoApiClientContext clientContext;

    public TodoTaskController(TodoApiClientContext clientContext)
    {
        this.clientContext = clientContext;
    }

    public IActionResult Index(int todoListId)
    {
        this.ViewData["Title"] = this.clientContext.GetEntityAsync<TodoList>(todoListId).Result!.Name;

        if (this.ModelState.IsValid)
        {
            var tasks = this.clientContext.GetEntitiesAsync<TodoTask>().Result!.Where(x => x.TodoListId == todoListId).ToList();

            var model = tasks.Select(todoTask => new TodoTaskViewModel
            {
                Instance = todoTask
            }).ToList();

            return this.View(model);
        }

        return this.View();
    }

    public async Task<IActionResult> AssignedTasks(TaskQueryOptions options)
    {
        this.ViewData["Title"] = "Assigned tasks";
        if (this.ModelState.IsValid)
        {
            var allLists = await this.clientContext.GetEntitiesAsync<TodoList>();
            var allTasks = await this.clientContext.GetEntitiesAsync<TodoTask>();
            var allM2M = await this.clientContext.GetEntitiesAsync<TagToTask>();

            if (allTasks == null || allLists == null)
            {
                return this.NotFound();
            }

            var listDictionary = allLists.ToDictionary(l => l.Id, l => l.Name ?? "Без назви");

            IQueryable<TodoTask> tasksQuery = allTasks.AsQueryable();

            if (!string.IsNullOrEmpty(options.FilterStatus))
            {
                tasksQuery = options.FilterStatus.ToLower() switch
                {
                    "pending" => tasksQuery.Where(t => !t.IsCompleted),
                    "completed" => tasksQuery.Where(t => t.IsCompleted),
                    _ => tasksQuery
                };
            }

            if (string.IsNullOrEmpty(options.SortBy) || options.SortBy.ToLower() == "list")
            {
                tasksQuery = tasksQuery.OrderBy(t => t.TodoListId).ThenBy(t => t.Name);
            }
            else if (options.SortBy.ToLower() == "deadline")
            {
                tasksQuery = tasksQuery.OrderBy(t => t.Deadline).ThenBy(t => t.Name);
            }
            else if (options.SortBy.ToLower() == "name")
            {
                tasksQuery = tasksQuery.OrderBy(t => t.Name).ThenBy(t => t.TodoListId);
            }

            if (options.SelectedTagId > 0)
            {
                var filteredTaskIds = allM2M!
                    .Where(m => m.TaskTagId == options.SelectedTagId)
                    .Select(m => m.TodoTaskId)
                    .Distinct()
                    .ToList();

                if (filteredTaskIds.Any())
                {
                    var relevantTodoTaskIds = allTasks
                        .Where(p => filteredTaskIds.Contains(p.Id))
                        .Select(p => p.Id)
                        .ToList();

                    tasksQuery = tasksQuery.Where(t => relevantTodoTaskIds.Contains(t.Id));
                }
                else
                {
                    tasksQuery = tasksQuery.Where(t => false);
                }
            }

            var filteredAndSortedTasks = tasksQuery.ToList();

            var model = filteredAndSortedTasks.Select(task => new ListNameTaskViewModel
            {
                Instance = task,
                ListName = listDictionary.GetValueOrDefault(task.TodoListId, "Unknown List")
            }).ToList();

            this.ViewBag.QueryOptions = options;
            return this.View(model);
        }

        return this.View();
    }

    public async Task<IActionResult> SearchTasks(SearchQueryOptions options)
    {
        this.ViewData["Title"] = "Search Tasks";

        var allLists = await this.clientContext.GetEntitiesAsync<TodoList>();
        var allTasks = await this.clientContext.GetEntitiesAsync<TodoTask>();

        if (allTasks == null || allLists == null)
        {
            return this.View(new List<ListNameTaskViewModel>());
        }

        var listDictionary = allLists.ToDictionary(l => l.Id, l => l.Name);

        IEnumerable<TodoTask> tasksQuery = allTasks;

        if (!string.IsNullOrWhiteSpace(options.SearchItem))
        {
            string searchTerm = options.SearchItem.Trim().ToLowerInvariant();
            tasksQuery = tasksQuery.Where(t =>
                !string.IsNullOrEmpty(t.Name) &&
                t.Name.ToLowerInvariant().Contains(searchTerm)
            );
        }

        var model = tasksQuery.Select(task => new ListNameTaskViewModel
        {
            Instance = task,
            ListName = listDictionary.GetValueOrDefault(task.TodoListId, "Unknown List")
        }).ToList();

        this.ViewBag.QueryOptions = options;

        return this.View(model);
    }

}
