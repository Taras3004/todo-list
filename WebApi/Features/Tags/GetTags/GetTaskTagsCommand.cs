using MediatR;

namespace WebApi.Features.Tags.GetTags;

public class GetTaskTagsCommand : IRequest<List<TaskTagResponse>>;
