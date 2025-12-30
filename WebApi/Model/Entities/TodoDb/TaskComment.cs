using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model.Entities.TodoDb;

public class TaskComment : BaseEntity
{
    public required string Content { get; set; }

    public DateTime Created { get; set; }

    [ForeignKey(nameof(Task))]
    public int TodoTaskId { get; set; }

    public TodoTask Task { get; set; }
}
