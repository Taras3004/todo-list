using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Lists.GetLists;

public class GetListsHandler(TodoListDbContext context) : IRequestHandler<GetListsCommand, List<TodoListDto>>
{
    public async Task<List<TodoListDto>> Handle(GetListsCommand request, CancellationToken cancellationToken)
    {
        var lists = await context.Todos.ToListAsync(cancellationToken);

        var listsDto = new List<TodoListDto>();

        foreach (var list in lists)
        {
            listsDto.Add(new TodoListDto()
            {
                Id = list.Id,
                Name = list.Name,
                Description = list.Description,
            });
        }

        return listsDto;
    }
}
