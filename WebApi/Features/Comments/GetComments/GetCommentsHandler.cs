using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Comments.GetComments;

public class GetCommentsHandler(TodoListDbContext context) : IRequestHandler<GetCommentsCommand, List<TaskCommentDto>>
{
    public async Task<List<TaskCommentDto>> Handle(GetCommentsCommand request, CancellationToken cancellationToken)
    {
        var comments = await context.TaskComments.ToListAsync(cancellationToken);

        var commentsDto = new List<TaskCommentDto>();

        foreach (var comment in comments)
        {
            commentsDto.Add(new TaskCommentDto()
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
