using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model.Entities.TodoDb;

public class TodoTaskPage : BaseEntity, IUserOwnedEntity
{
    [ForeignKey(nameof(Task))]
    public int TodoTaskId { get; set; }

    public required TodoTask Task { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public required string UserId { get; set; }
}
