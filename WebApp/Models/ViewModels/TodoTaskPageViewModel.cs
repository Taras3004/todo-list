using WebApi.Entities.TodoDb;

namespace WebApp.Models.ViewModels;

public class TodoTaskPageViewModel : IReturnUrlViewModel<TodoTaskPage>
{
    public TodoTaskPage Instance { get; set; }

    public string? ReturnUrl { get; set; }

    public List<TaskTag> Tags { get; init; }

    public List<TaskTag> SelectedTags { get; init; }

    public int TodoTaskPageId { get; init; }

    public int TaskTagId { get; init; }

    public List<TaskComment>? Comments { get; init; }
}
