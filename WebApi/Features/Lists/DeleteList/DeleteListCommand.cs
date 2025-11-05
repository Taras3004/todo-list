using MediatR;
using WebApi.Model.Dto;

namespace WebApi.Features.Lists.DeleteList;

public class DeleteListCommand(int id) : IRequest<bool>
{
    public int Id { get; } = id;
}
