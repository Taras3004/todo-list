using MediatR;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Comments.CreateComment;

public class CreateTaskCommentHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<CreateTaskCommentCommand, TaskCommentResponse>
{
    public async Task<TaskCommentResponse> Handle(CreateTaskCommentCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        TaskComment taskComment = new TaskComment()
        {
            Content = request.Content,
            Created = DateTime.Now,
            TodoTaskId = request.TodoTaskId,
            UserId = userId,
        };

        context.TaskComments.Add(taskComment);

        await context.SaveChangesAsync(cancellationToken);

        return new TaskCommentResponse()
        {
            Id = taskComment.Id,
            Content = taskComment.Content,
            Created = taskComment.Created,
            TodoTaskId = taskComment.TodoTaskId,
        };
    }
}
