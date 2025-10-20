using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities.TodoDb;

public class TodoTask : BaseEntity
{
    [ForeignKey(nameof(TodoList))]
    public int TodoListId { get; set; }

    [ForeignKey(nameof(TaskTag))]
    public int TaskTagId { get; set; }

    [StringLength(80)]
    public string Name { get; set; }

    public DateTime Deadline { get; set; }

    public bool IsCompleted { get; set; }
}
