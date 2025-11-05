using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.Comments.GetComments;

public class GetCommentsCommand : IRequest<List<TaskCommentDto>>;
