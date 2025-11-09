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
using WebApi.Model.Dto;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
    {
        var taskDto = await mediator.Send(command);

        return this.CreatedAtAction(nameof(this.GetTask), new { id = taskDto.Id }, taskDto);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var query = new GetTaskByIdCommand(id);

        var taskDto = await mediator.Send(query);

        return taskDto == null ? this.NotFound() : this.Ok(taskDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskCommand command)
    {
        var taskDto = await mediator.Send(command);

        return taskDto == null ? this.NotFound() : this.Ok(taskDto);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTask(int id)
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
        var tasksDto = await mediator.Send(command);
        return this.Ok(tasksDto);
    }

    [HttpPost("{taskId}/tags")]
    public async Task<IActionResult> AddTag([FromRoute] int taskId, [FromBody] AddTagToTaskDto body)
    {
        var command = new AddTagToTaskCommand()
        {
            TagId = body.TagId,
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
