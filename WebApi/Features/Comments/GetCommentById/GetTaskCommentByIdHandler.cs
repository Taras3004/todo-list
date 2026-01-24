using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Comments.GetCommentById;

public class GetTaskCommentByIdHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<GetTaskCommentByIdCommand, TaskCommentResponse?>
{
    public async Task<TaskCommentResponse?> Handle(GetTaskCommentByIdCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        var comment = await context.TaskComments.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == userId, cancellationToken);

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
