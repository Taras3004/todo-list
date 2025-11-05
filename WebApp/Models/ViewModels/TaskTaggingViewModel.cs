using WebApi.Model.Entities.TodoDb;

namespace WebApp.Models.ViewModels;

public class TaskTaggingViewModel : IReturnUrlViewModel<TagToTask>
{
    public TagToTask Instance { get; set; }
    public string ReturnUrl { get; set; }
    public int TodoTaskId { get; set; }
    public int TaskTagId { get; set; }
}
