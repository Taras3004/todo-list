using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.Tags.GetTags;

public class GetTagsCommand : IRequest<List<TaskTagDto>>;
