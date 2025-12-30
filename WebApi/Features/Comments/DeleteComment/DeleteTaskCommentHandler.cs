using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Comments.DeleteComment;

public class DeleteCommentHandler(TodoListDbContext context) : IRequestHandler<DeleteTaskCommentCommand, bool>
{
    public async Task<bool> Handle(DeleteTaskCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await context.TaskComments.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (comment == null)
        {
            return false;
        }

        context.TaskComments.Remove(comment);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
