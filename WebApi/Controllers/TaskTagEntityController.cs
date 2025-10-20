using Microsoft.AspNetCore.Mvc;
using WebApi.Entities.TodoDb;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskTagEntityController : AbstractEntityController<TaskTag>
{
    public TaskTagEntityController(TodoListDbContext dbContext) : base(dbContext)
    {
    }

}
