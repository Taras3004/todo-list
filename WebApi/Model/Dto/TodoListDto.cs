using MediatR;

namespace WebApi.Model.Dto;

public record TodoListDto : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
