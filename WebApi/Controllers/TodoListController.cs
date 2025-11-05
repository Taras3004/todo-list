using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Features.Lists.CreateList;
using WebApi.Features.Lists.DeleteList;
using WebApi.Features.Lists.GetListById;
using WebApi.Features.Lists.GetLists;
using WebApi.Features.Lists.UpdateList;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoListController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateListCommand command)
    {
        var taskDto = await mediator.Send(command);

        return this.CreatedAtAction(nameof(this.GetTask), new { id = taskDto.Id }, taskDto);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var query = new GetListByIdCommand(id);

        var taskDto = await mediator.Send(query);

        return taskDto == null ? this.NotFound() : this.Ok(taskDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateListCommand command)
    {
        var taskDto = await mediator.Send(command);

        return taskDto == null ? this.NotFound() : this.Ok(taskDto);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var command = new DeleteListCommand(id);

        var isDeleted = await mediator.Send(command);

        return isDeleted ? this.NoContent() : this.NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var command = new GetListsCommand();
        var tasksDto = await mediator.Send(command);

        return this.Ok(tasksDto);
    }
}
