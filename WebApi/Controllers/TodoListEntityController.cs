using Microsoft.AspNetCore.Mvc;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoListEntityController : AbstractEntityController<TodoList>
{
    public TodoListEntityController(TodoListDbContext dbContext) : base(dbContext)
    {
    }
}
