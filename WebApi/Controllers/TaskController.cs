using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Features.Tasks.AddTag;
using WebApi.Features.Tasks.CreateTask;
using WebApi.Features.Tasks.DeleteTask;
using WebApi.Features.Tasks.GetTags;
using WebApi.Features.Tasks.GetTaskById;
using WebApi.Features.Tasks.GetTasks;
using WebApi.Features.Tasks.RemoveTag;
using WebApi.Features.Tasks.UpdateTask;
using WebApi.Model.Dto.Requests.Tasks;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        CreateTaskCommand command = new CreateTaskCommand(
            request.Name,
            DateTime.Today.AddDays(2).AddSeconds(-1),
            request.Description,
            request.TodoListId
        );
        var taskResponse = await mediator.Send(command);

        return this.Ok(taskResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask([FromRoute] int id)
    {
        var query = new GetTaskByIdCommand(id);

        var taskResponse = await mediator.Send(query);

        return taskResponse == null ? this.NotFound() : this.Ok(taskResponse);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskRequest request)
    {
        UpdateTaskCommand command = new UpdateTaskCommand(request.Id, request.Name, request.Deadline,
        request.IsCompleted, request.Description);

        var taskResponse = await mediator.Send(command);

        return taskResponse == null ? this.NotFound() : this.Ok(taskResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask([FromRoute] int id)
    {
        var command = new DeleteTaskByIdCommand(id);

        var isDeleted = await mediator.Send(command);

        return isDeleted ? this.NoContent() : this.NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks([FromQuery] int todoListId)
    {
        var command = new GetTasksCommand()
        {
            TodoListId = todoListId
        };
        var taskResponse = await mediator.Send(command);
        return this.Ok(taskResponse);
    }

    [HttpPost("{taskId}/tags")]
    public async Task<IActionResult> AddTag([FromRoute] int taskId, [FromBody] AddTagToTaskRequest request)
    {
        var command = new AddTagToTaskCommand()
        {
            TagId = request.TagId,
            TaskId = taskId,
        };

        var isCreated = await mediator.Send(command);

        return isCreated ? this.Ok() : this.NotFound("No such tag or task");
    }

    [HttpDelete("{taskId}/tags/{tagId}")]
    public async Task<IActionResult> RemoveTag([FromRoute] int taskId, [FromRoute] int tagId)
    {
        var command = new RemoveTagToTaskCommand()
        {
            TagId = tagId,
            TaskId = taskId,
        };

        var isDeleted = await mediator.Send(command);

        return isDeleted ? this.NoContent() : this.NotFound();
    }

    [HttpGet("{taskId}/tags")]
    public async Task<IActionResult> GetTags([FromRoute] int taskId)
    {
        var command = new GetTaskTagsCommand() { TaskId = taskId };
        var tags = await mediator.Send(command);

        return this.Ok(tags);
    }
}
