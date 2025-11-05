using WebApi.Model.Entities.TodoDb;

namespace WebApp.Models.ViewModels;

public class TaskCommentViewModel : IReturnUrlViewModel<TaskComment>
{
    public TaskComment Instance { get; set; }
    public string ReturnUrl { get; set; }
}
