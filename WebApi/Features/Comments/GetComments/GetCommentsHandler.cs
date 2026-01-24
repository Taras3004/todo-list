using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Comments.GetComments;

public class GetCommentsHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<GetCommentsCommand, List<TaskCommentResponse>>
{
    public async Task<List<TaskCommentResponse>> Handle(GetCommentsCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        var comments = await context.TaskComments.Where(x => x.UserId == userId)
                                        .ToListAsync(cancellationToken);

        var commentsDto = new List<TaskCommentResponse>();

        foreach (var comment in comments)
        {
            commentsDto.Add(new TaskCommentResponse()
            {
                Id = comment.Id,
                Content = comment.Content,
                Created = comment.Created,
                TodoTaskId = comment.TodoTaskId,
            });
        }

        return commentsDto;
    }
}
