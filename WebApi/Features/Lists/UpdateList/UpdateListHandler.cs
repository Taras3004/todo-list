using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Lists.UpdateList;

public class UpdateListHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<UpdateListCommand, TodoListResponse?>
{
    public async Task<TodoListResponse?> Handle(UpdateListCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        var existingList = await context.Todos
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == userId, cancellationToken);

        if (existingList == null)
        {
            return null;
        }

        existingList.Name = request.Name;
        existingList.Description = request.Description;

        await context.SaveChangesAsync(cancellationToken);

        return new TodoListResponse()
        {
            Id = existingList.Id,
            Name = existingList.Name,
            Description = existingList.Description,
        };
    }
}
