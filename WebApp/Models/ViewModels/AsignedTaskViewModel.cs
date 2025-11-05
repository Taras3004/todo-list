using WebApi.Model.Entities.TodoDb;

namespace WebApp.Models.ViewModels;

public class ListNameTaskViewModel : IViewModel<TodoTask>
{
    public TodoTask Instance { get; set; }
    public string? ListName { get; set; }
}
