using Microsoft.AspNetCore.Mvc;
using WebApi.Model.Entities.TodoDb;
using WebApp.Models.ApiClients.TodoListApiClient;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers.EditControllers;

public class TodoTaskEditController : AbstractEditController<TodoTask, TodoTaskViewModel>
{
    private readonly TodoApiClientContext clientContext;

    public TodoTaskEditController(TodoApiClientContext clientContext) : base(clientContext)
    {
        this.clientContext = clientContext;
    }

    protected override string ViewName { get; } = "EditTodoTask";


    [HttpGet("CreateWithList/{todoListId}")]
    public IActionResult CreateTodoTask(string returnUrl, int todoListId)
    {
        this.ViewData["title"] = "Create new";
        var vm = new TodoTaskViewModel
        {
            Instance = new TodoTask { TodoListId = todoListId, Deadline = DateTime.Now.Date.AddDays(2).AddSeconds(-1) },
            ReturnUrl = returnUrl
        };

        return this.View(this.ViewName, vm);
    }

    public async Task<IActionResult> ToggleComplete(int id, bool isCompleted)
    {
        var task = await this.clientContext.GetEntityAsync<TodoTask>(id);

        if (task != null || !this.ModelState.IsValid)
        {
            this.NotFound();
        }

        task!.IsCompleted = isCompleted;

        await this.clientContext.SaveEntityAsync(task);

        return this.NoContent();
    }
}
