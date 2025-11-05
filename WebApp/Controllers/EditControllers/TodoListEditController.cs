using WebApi.Model.Entities.TodoDb;
using WebApp.Models.ApiClients.TodoListApiClient;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers.EditControllers;

public class TodoListEditController : AbstractEditController<TodoList, TodoListViewModel>
{
    private readonly TodoApiClientContext clientContext;

    public TodoListEditController(TodoApiClientContext clientContext) : base(clientContext)
    {
        this.clientContext = clientContext;
    }

    protected override string ViewName { get; } = "EditTodoList";
}
