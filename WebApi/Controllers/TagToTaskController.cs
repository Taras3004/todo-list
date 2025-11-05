using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Features.TagToTask.CreateTagToTask;
using WebApi.Features.TagToTask.DeleteTagToTask;
using WebApi.Features.TagToTask.GetTagToTaskById;
using WebApi.Features.TagToTask.GetTagToTasks;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagToTaskController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTagToTaskCommand command)
    {
        var taskDto = await mediator.Send(command);

        return this.CreatedAtAction(nameof(this.GetTask), new { id = taskDto.Id }, taskDto);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var query = new GetTagToTaskByIdCommand(id);

        var taskDto = await mediator.Send(query);

        return taskDto == null ? this.NotFound() : this.Ok(taskDto);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var command = new DeleteTagToTaskCommand(id);

        var isDeleted = await mediator.Send(command);

        return isDeleted ? this.NoContent() : this.NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var command = new GetTagToTasksCommand();
        var tasksDto = await mediator.Send(command);

        return this.Ok(tasksDto);
    }
}
