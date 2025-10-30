using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities.TodoDb;

public class TaskComment : BaseEntity
{
    public string Content { get; set; }

    public DateTime Created { get; set; }

    [ForeignKey(nameof(TodoTask))]
    public int TodoTaskId { get; set; }
}
