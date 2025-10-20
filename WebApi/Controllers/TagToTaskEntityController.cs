using Microsoft.AspNetCore.Mvc;
using WebApi.Entities.TodoDb;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagToTaskEntityController : AbstractEntityController<TagToTask>
{
    public TagToTaskEntityController(TodoListDbContext dbContext) : base(dbContext)
    { }
}
