using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WebApi.Model.Entities.UsersDb;

namespace WebApi.Model.Entities.TodoDb;

public class TodoTaskPage : BaseEntity, IUserOwnedEntity
{
    [ForeignKey(nameof(Task))]
    public int TodoTaskId { get; set; }

    public TodoTask Task { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [ValidateNever]
    public string UserId { get; set; }

    [ValidateNever]
    public virtual ApplicationUser User { get; set; }
}
