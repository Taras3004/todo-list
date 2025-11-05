using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Lists.GetListById;

public class GetListByIdHandler(TodoListDbContext context) : IRequestHandler<GetListByIdCommand, TodoListDto?>
{
    public async Task<TodoListDto?> Handle(GetListByIdCommand request, CancellationToken cancellationToken)
    {
        var list = await context.Todos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (list == null)
        {
            return null;
        }

        return new TodoListDto()
        {
            Id = list.Id,
            Name = list.Name,
            Description = list.Description,
        };
    }
}
