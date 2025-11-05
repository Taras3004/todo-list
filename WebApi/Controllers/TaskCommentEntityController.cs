using Microsoft.AspNetCore.Mvc;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskCommentEntityController : AbstractEntityController<TaskComment>
{
    public TaskCommentEntityController(TodoListDbContext dbContext) : base(dbContext)
    {
    }
}
