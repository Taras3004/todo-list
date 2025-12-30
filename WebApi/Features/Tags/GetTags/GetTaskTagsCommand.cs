using MediatR;
using WebApi.Model.Dto.Responses;

namespace WebApi.Features.Tags.GetTags;

public class GetTaskTagsCommand : IRequest<List<TaskTagResponse>>;
