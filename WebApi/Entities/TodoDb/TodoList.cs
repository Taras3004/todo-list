using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WebApi.Entities.UsersDb;

namespace WebApi.Entities.TodoDb;

public class TodoList : BaseEntity, IUserOwnedEntity
{
    [StringLength(80)]
    public string Name { get; set; }

    [StringLength(80)]
    public string Description { get; set; }

    [ValidateNever]
    public string UserId { get; set; }

    [ValidateNever]
    public virtual ApplicationUser User { get; set; }
}
