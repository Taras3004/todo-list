using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Features.Tasks.CreateTask;
using WebApi.Features.Tasks.DeleteTask;
using WebApi.Features.Tasks.GetTaskById;
using WebApi.Features.Tasks.GetTasks;
using WebApi.Features.Tasks.UpdateTask;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var query = new GetTaskByIdCommand() { Id = id };

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
        var command = new DeleteTaskByIdCommand() { Id = id };

        var isDeleted = await mediator.Send(command);

        return isDeleted ? this.NoContent() : this.NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var command = new GetTasksCommand();
        var tasksDto = await mediator.Send(command);

        return this.Ok(tasksDto);
    }
}
