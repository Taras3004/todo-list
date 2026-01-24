using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Lists.GetLists;

public class GetListsHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<GetListsCommand, List<TodoListResponse>>
{
    public async Task<List<TodoListResponse>> Handle(GetListsCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        var lists = await context.Todos
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);

        var listsDto = new List<TodoListResponse>();

        foreach (var list in lists)
        {
            listsDto.Add(new TodoListResponse()
            {
                Id = list.Id,
                Name = list.Name,
                Description = list.Description,
            });
        }

        return listsDto;
    }
}
