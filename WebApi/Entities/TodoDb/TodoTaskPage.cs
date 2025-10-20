using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities.TodoDb;

public class TodoTaskPage : BaseEntity
{
    public int TodoTaskId { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }
}
