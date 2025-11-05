using WebApi.Model.Entities.TodoDb;
using WebApp.Models.ApiClients.TodoListApiClient;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers.EditControllers;

public class TaskTagEditController : AbstractEditController<TaskTag, TaskTagViewModel>
{
    public TaskTagEditController(TodoApiClientContext clientContext) : base(clientContext)
    {
    }

    protected override string ViewName { get; } = "EditTaskTag";
}
