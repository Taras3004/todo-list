using WebApi.Model.Entities.TodoDb;

namespace WebApp.Models.ViewModels;

public interface IReturnUrlViewModel<TEntity> : IViewModel<TEntity>
    where TEntity : BaseEntity
{
    public string? ReturnUrl { get; set; }
}
