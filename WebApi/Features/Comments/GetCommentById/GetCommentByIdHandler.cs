using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Comments.GetCommentById;

public class GetCommentByIdHandler(TodoListDbContext context) : IRequestHandler<GetCommentByIdCommand, TaskCommentDto?>
{
    public async Task<TaskCommentDto?> Handle(GetCommentByIdCommand request, CancellationToken cancellationToken)
    {
        var comment = await context.TaskComments.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (comment == null)
        {
            return null;
        }

        return new TaskCommentDto()
        {
            Id = comment.Id,
            Content = comment.Content,
            Created = comment.Created,
            TodoTaskId = comment.TodoTaskId,
        };
    }
}
