using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tags.DeleteTag;

public class DeleteTaskTagHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<DeleteTaskTagCommand, bool>
{
    public async Task<bool> Handle(DeleteTaskTagCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        var tag = await context.TaskTags.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == userId, cancellationToken);

        if (tag == null)
        {
            return false;
        }

        context.TaskTags.Remove(tag);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
