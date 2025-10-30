using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities.TodoDb;

public class TodoTaskPage : BaseEntity, IUserOwnedEntity
{
    public int TodoTaskId { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [ValidateNever]
    public string UserId { get; set; }

    [ValidateNever]
    public virtual ApplicationUser User { get; set; }
}
