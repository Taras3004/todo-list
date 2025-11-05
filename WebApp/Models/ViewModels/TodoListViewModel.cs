using WebApi.Model.Entities.TodoDb;

namespace WebApp.Models.ViewModels;

public class TodoListViewModel : IReturnUrlViewModel<TodoList>
{
    public TodoList Instance { get; set; } = new();

    public string? ReturnUrl { get; set; }
}
