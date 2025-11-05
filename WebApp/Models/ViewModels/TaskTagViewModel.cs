using WebApi.Model.Entities.TodoDb;

namespace WebApp.Models.ViewModels;

public class TaskTagViewModel : IReturnUrlViewModel<TaskTag>
{
    public TaskTag Instance { get; set; } = new();
    public string? ReturnUrl { get; set; }
}
