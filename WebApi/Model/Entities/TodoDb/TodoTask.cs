using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model.Entities.TodoDb;

public class TodoTask : BaseEntity
{
    [ForeignKey(nameof(TodoList))]
    public int TodoListId { get; set; }

    [StringLength(80)]
    public required string Name { get; set; }

    public DateTime Deadline { get; set; }

    public bool IsCompleted { get; set; }

    public TodoList TodoList { get; set; }

    public TodoTaskPage TaskPage { get; set; }

    public ICollection<TagToTask> TagToTasks { get; set; } = [];
}
