using System.ComponentModel.DataAnnotations;

namespace WebApi.Model.Entities.TodoDb;

public class TaskTag : BaseEntity, IUserOwnedEntity
{
    [StringLength(80)]
    public required string Tag { get; set; }

    public string? Color { get; set; }

    public required string UserId { get; set; }
}
