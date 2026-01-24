using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tags.UpdateTag;

public class UpdateTaskTagHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<UpdateTaskTagCommand, TaskTagResponse?>
{
    public async Task<TaskTagResponse?> Handle(UpdateTaskTagCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        var existingTag = await context.TaskTags
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == userId, cancellationToken);

        if (existingTag == null)
        {
            return null;
        }

        existingTag.Tag = request.Tag;

        await context.SaveChangesAsync(cancellationToken);

        return new TaskTagResponse()
        {
            Id = existingTag.Id,
            Tag = existingTag.Tag,
            Color = existingTag.Color,
        };
    }
}
