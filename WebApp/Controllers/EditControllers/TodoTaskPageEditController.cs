using WebApi.Entities.TodoDb;
using WebApp.Models.ApiClients.TodoListApiClient;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers.EditControllers;

public class TodoTaskPageEditController : AbstractEditController<TodoTaskPage, TodoTaskPageViewModel>
{
    public TodoTaskPageEditController(TodoApiClientContext clientContext) : base(clientContext)
    { }

    protected override string ViewName { get; } = "EditTodoTaskPage";
}
