using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Features.Comments.CreateComment;
using WebApi.Features.Comments.DeleteComment;
using WebApi.Features.Comments.GetCommentById;
using WebApi.Features.Comments.GetComments;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskCommentController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateCommentCommand command)
    {
        var taskDto = await mediator.Send(command);

        return this.CreatedAtAction(nameof(this.GetTask), new { id = taskDto.Id }, taskDto);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var query = new GetCommentByIdCommand(id);

        var taskDto = await mediator.Send(query);

        return taskDto == null ? this.NotFound() : this.Ok(taskDto);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var command = new DeleteCommentCommand(id);

        var isDeleted = await mediator.Send(command);

        return isDeleted ? this.NoContent() : this.NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var command = new GetCommentsCommand();
        var tasksDto = await mediator.Send(command);

        return this.Ok(tasksDto);
    }
}
