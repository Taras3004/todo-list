using MediatR;
using WebApi.Model.Dto.Responses;

namespace WebApi.Features.Comments.GetComments;

public class GetCommentsCommand : IRequest<List<TaskCommentResponse>>;
