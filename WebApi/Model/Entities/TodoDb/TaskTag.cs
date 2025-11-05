using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WebApi.Model.Entities.UsersDb;

namespace WebApi.Model.Entities.TodoDb;

public class TaskTag : BaseEntity, IUserOwnedEntity
{
    public string Tag { get; set; }

    [ValidateNever]
    public string UserId { get; set; }

    [ValidateNever]
    public virtual ApplicationUser User { get; set; }
}
