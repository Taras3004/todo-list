using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model.Entities.TodoDb;

public class TagToTask : BaseEntity
{
    [ForeignKey(nameof(Tag))]
    public int TaskTagId { get; set; }
    public TaskTag Tag { get; set; }

    [ForeignKey(nameof(Task))]
    public int TodoTaskId { get; set; }
    public TodoTask Task { get; set; }
}
