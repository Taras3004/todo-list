using WebApi.Model.Entities.TodoDb;

namespace WebApp.Models.ViewModels;

public interface IViewModel<TEntity>
    where TEntity : BaseEntity
{
    TEntity Instance { get; set; }
}
