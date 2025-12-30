using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Comments.GetCommentById;

public class GetTaskCommentByIdHandler(TodoListDbContext context) : IRequestHandler<GetTaskCommentByIdCommand, TaskCommentResponse?>
{
    public async Task<TaskCommentResponse?> Handle(GetTaskCommentByIdCommand request, CancellationToken cancellationToken)
    {
        var comment = await context.TaskComments.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (comment == null)
        {
            return null;
        }

        return new TaskCommentResponse()
        {
            Id = comment.Id,
            Content = comment.Content,
            Created = comment.Created,
            TodoTaskId = comment.TodoTaskId,
        };
    }
}
