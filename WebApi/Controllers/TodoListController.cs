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
    public async Task<IActionResult> CreateList([FromBody] CreateListRequest request)
    {
        CreateListCommand command = new CreateListCommand(
            request.Name,
            request.Description);

        var taskResponse = await mediator.Send(command);

        return this.Ok(taskResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetList([FromRoute] int id)
    {
        var query = new GetListByIdCommand(id);

        var taskResponse = await mediator.Send(query);

        return taskResponse == null ? this.NotFound() : this.Ok(taskResponse);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateList([FromBody] UpdateListRequest request)
    {
        UpdateListCommand command = new UpdateListCommand(request.Id, request.Name, request.Description);

        var taskResponse = await mediator.Send(command);

        return taskResponse == null ? this.NotFound() : this.Ok(taskResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteList(int id)
    {
        var command = new DeleteListCommand(id);

        var isDeleted = await mediator.Send(command);

        return isDeleted ? this.NoContent() : this.NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetLists()
    {
        var command = new GetListsCommand();
        var tasksDto = await mediator.Send(command);

        return this.Ok(tasksDto);
    }
}
