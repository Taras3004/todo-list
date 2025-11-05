using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tags.DeleteTag;

public class DeleteTagHandler(TodoListDbContext context) : IRequestHandler<DeleteTagCommand, bool>
{
    public async Task<bool> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await context.TaskTags.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (tag == null)
        {
            return false;
        }

        context.TaskTags.Remove(tag);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
