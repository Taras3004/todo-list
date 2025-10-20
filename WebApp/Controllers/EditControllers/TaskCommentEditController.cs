using Microsoft.AspNetCore.Mvc;
using WebApi.Entities.TodoDb;
using WebApp.Models.ApiClients.TodoListApiClient;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers.EditControllers;
public class TaskCommentEditController : AbstractEditController<TaskComment, TaskCommentViewModel>
{
    public TaskCommentEditController(TodoApiClientContext clientContext) : base(clientContext)
    {
    }

    protected override string ViewName { get; } = "EditTaskComment";

    [HttpGet("CreateWithTash/{todoTaskId}")]
    public IActionResult CreateComment(string returnUrl, int todoTaskId)
    {
        this.ViewData["title"] = "Create new";
        var vm = new TaskCommentViewModel()
        {
            Instance = new TaskComment() { TodoTaskId = todoTaskId, Created = DateTime.Now, },
            ReturnUrl = returnUrl
        };

        return this.View(this.ViewName, vm);
    }
}
