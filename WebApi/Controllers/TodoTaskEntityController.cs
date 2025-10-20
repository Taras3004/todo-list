using Microsoft.AspNetCore.Mvc;
using WebApi.Entities.TodoDb;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoTaskEntityController : AbstractEntityController<TodoTask>
{
    public TodoTaskEntityController(TodoListDbContext dbContext) : base(dbContext) { }
}
