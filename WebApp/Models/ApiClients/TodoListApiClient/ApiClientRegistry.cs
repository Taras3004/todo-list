using WebApi.Model.Entities.TodoDb;

namespace WebApp.Models.ApiClients.TodoListApiClient;

public class ApiClientRegistry
{
    private readonly Dictionary<Type, object> apiClients = [];

    public IReadOnlyDictionary<Type, object> ApiClients() => this.apiClients;

    public void AddApiClient<TEntity>(ApiClient<TEntity> apiClient)
        where TEntity : BaseEntity
    {
        this.apiClients[typeof(TEntity)] = apiClient;
    }

    public ApiClient<TEntity> GetApiClient<TEntity>()
        where TEntity : BaseEntity
    {
        return (ApiClient<TEntity>)this.apiClients[typeof(TEntity)];
    }
}
