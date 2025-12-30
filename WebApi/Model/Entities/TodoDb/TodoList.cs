using System.ComponentModel.DataAnnotations;

namespace WebApi.Model.Entities.TodoDb;

public class TodoList : BaseEntity, IUserOwnedEntity
{
    [StringLength(80)]
    public required string Name { get; set; }

    [StringLength(80)]
    public string? Description { get; set; }

    public required string UserId { get; set; }
}
