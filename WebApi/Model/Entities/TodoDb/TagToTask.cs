using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities.TodoDb;

public class TagToTask : BaseEntity
{
    [ForeignKey(nameof(TaskTag))]
    public int TaskTagId { get; set; }

    [ForeignKey(nameof(TodoTask))]
    public int TodoTaskId { get; set; }
}
