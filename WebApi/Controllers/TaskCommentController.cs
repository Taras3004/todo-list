using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Features.Comments.CreateComment;
using WebApi.Features.Comments.DeleteComment;
using WebApi.Features.Comments.GetCommentById;
using WebApi.Features.Comments.GetComments;
using WebApi.Model.Dto.Requests.TaskComment;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskCommentController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CreateTaskCommentRequest request)
    {
        CreateTaskCommentCommand command = new CreateTaskCommentCommand(request.Content, request.TodoTaskId);

        var taskCommentResponse = await mediator.Send(command);

        return this.Ok(taskCommentResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetComment([FromRoute] int id)
    {
        var query = new GetTaskCommentByIdCommand(id);

        var taskCommentResponse = await mediator.Send(query);

        return taskCommentResponse == null ? this.NotFound() : this.Ok(taskCommentResponse);
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
        var taskCommentsResponse = await mediator.Send(command);

        return this.Ok(taskCommentsResponse);
    }
}
