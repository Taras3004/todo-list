using WebApi.Entities.TodoDb;

namespace WebApp.Models.ViewModels;

public class TodoTaskViewModel : IReturnUrlViewModel<TodoTask>
{
    public TodoTask Instance { get; set; } = new();

    public string? ReturnUrl { get; set; }

    public int TodoListId { get; set; }
}
