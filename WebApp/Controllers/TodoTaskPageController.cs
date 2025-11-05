using Microsoft.AspNetCore.Mvc;
using WebApi.Model.Entities.TodoDb;
using WebApp.Models.ApiClients.TodoListApiClient;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers;

public class TodoTaskPageController : Controller
{
    private readonly TodoApiClientContext clientContext;

    public TodoTaskPageController(TodoApiClientContext clientContext)
    {
        this.clientContext = clientContext;
    }

    public async Task<IActionResult> Index(int taskId, string returnUrl)
    {
        var todoPages = await this.clientContext.GetEntitiesAsync<TodoTaskPage>();
        var tags = await this.clientContext.GetEntitiesAsync<TaskTag>();
        var comments = await this.clientContext.GetEntitiesAsync<TaskComment>();
        var m2M = await this.clientContext.GetEntitiesAsync<TagToTask>();

        var instance = todoPages!.FirstOrDefault(x => x.TodoTaskId == taskId);

        if (instance == null)
        {
            return this.NotFound();
        }

        var assignedTagIds = m2M!
                .Where(x => x.TodoTaskId == taskId)
                .Select(x => x.TaskTagId)
                .ToList();

        var selectedTags = tags!
            .Where(x => assignedTagIds.Contains(x.Id))
            .ToList();

        var selectedComments = comments
            .Where(x => x.TodoTaskId == taskId)
            .ToList();

        return this.View(new TodoTaskPageViewModel()
        {
            Instance = instance,
            ReturnUrl = returnUrl,
            Tags = tags!,
            SelectedTags = selectedTags,
            Comments = selectedComments,
        });
    }

    public Task<IActionResult> Back(string returnUrl)
    {
        if (string.IsNullOrEmpty(returnUrl))
        {
            this.RedirectToAction("Index", "Home");
        }

        return Task.FromResult<IActionResult>(this.Redirect(returnUrl));
    }
}
