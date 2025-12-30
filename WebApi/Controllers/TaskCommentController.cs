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
    public async Task<IActionResult> CreateComment([FromBody] CreateTaskCommentRequest request)
    {
        CreateTaskCommentCommand command = new CreateTaskCommentCommand(request.Content, request.TodoTaskId);

        var taskDto = await mediator.Send(command);

        return this.Ok(taskDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetComment([FromRoute] int id)
    {
        var query = new GetTaskCommentByIdCommand(id);

        var taskResponse = await mediator.Send(query);

        return taskResponse == null ? this.NotFound() : this.Ok(taskResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int id)
    {
        var command = new DeleteTaskCommentCommand(id);

        var isDeleted = await mediator.Send(command);

        return isDeleted ? this.NoContent() : this.NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetComments()
    {
        var command = new GetCommentsCommand();
        var tasksDto = await mediator.Send(command);

        return this.Ok(tasksDto);
    }
}
