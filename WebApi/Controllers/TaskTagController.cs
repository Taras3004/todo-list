using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Features.Tags.CreateTag;
using WebApi.Features.Tags.DeleteTag;
using WebApi.Features.Tags.GetTagById;
using WebApi.Features.Tags.GetTags;
using WebApi.Features.Tags.UpdateTag;
using WebApi.Model.Dto.Requests.TaskTags;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskTagController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskTagRequest request)
    {
        CreateTaskTagCommand command = new CreateTaskTagCommand(request.Tag);

        var taskTagResponse = await mediator.Send(command);

        return this.Ok(taskTagResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask([FromRoute] int id)
    {
        var query = new GetTaskTagByIdCommand(id);

        var taskTagResponse = await mediator.Send(query);

        return taskTagResponse == null ? this.NotFound() : this.Ok(taskTagResponse);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskTagRequest request)
    {
        UpdateTaskTagCommand command = new UpdateTaskTagCommand(request.Id, request.Tag);

        var taskTagResponse = await mediator.Send(command);

        return taskTagResponse == null ? this.NotFound() : this.Ok(taskTagResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask([FromRoute] int id)
    {
        var command = new DeleteTaskTagCommand(id);

        var isDeleted = await mediator.Send(command);

        return isDeleted ? this.NoContent() : this.NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var command = new GetTaskTagsCommand();
        var taskTagsResponse = await mediator.Send(command);

        return this.Ok(taskTagsResponse);
    }
}
