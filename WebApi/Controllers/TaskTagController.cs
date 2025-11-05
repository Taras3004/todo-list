using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Features.Tags.CreateTag;
using WebApi.Features.Tags.DeleteTag;
using WebApi.Features.Tags.GetTagById;
using WebApi.Features.Tags.GetTags;
using WebApi.Features.Tags.UpdateTag;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskTagController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTagCommand command)
    {
        var taskDto = await mediator.Send(command);

        return this.CreatedAtAction(nameof(this.GetTask), new { id = taskDto.Id }, taskDto);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var query = new GetTagByIdCommand(id);

        var taskDto = await mediator.Send(query);

        return taskDto == null ? this.NotFound() : this.Ok(taskDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateTagCommand command)
    {
        var taskDto = await mediator.Send(command);

        return taskDto == null ? this.NotFound() : this.Ok(taskDto);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var command = new DeleteTagCommand(id);

        var isDeleted = await mediator.Send(command);

        return isDeleted ? this.NoContent() : this.NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var command = new GetTagsCommand();
        var tasksDto = await mediator.Send(command);

        return this.Ok(tasksDto);
    }
}
