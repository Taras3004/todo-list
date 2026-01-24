using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dto.Responses;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Features.Tags.GetTags;

public class GetTagsHandler(TodoListDbContext context, IHttpContextAccessor http) : IRequestHandler<GetTaskTagsCommand, List<TaskTagResponse>>
{
    public async Task<List<TaskTagResponse>> Handle(GetTaskTagsCommand request, CancellationToken cancellationToken)
    {
        var user = (http.HttpContext?.User) ?? throw new UnauthorizedAccessException();
        var userId = user.GetUserId();

        var tags = await context.TaskTags.Where(x => x.UserId == userId)
                                        .ToListAsync(cancellationToken);

        var tagsDto = new List<TaskTagResponse>();

        foreach (var tag in tags)
        {
            tagsDto.Add(new TaskTagResponse()
            {
                Id = tag.Id,
                Tag = tag.Tag,
                Color = tag.Color,
            });
        }

        return tagsDto;
    }
}
