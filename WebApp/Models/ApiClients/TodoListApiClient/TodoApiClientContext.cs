using WebApi.Model.Entities.TodoDb;
using WebApp.Models.ApiClients.TodoListApiClient.Registrations;

namespace WebApp.Models.ApiClients.TodoListApiClient;

public class TodoApiClientContext
{
    private readonly IReadOnlyDictionary<Type, object> apiClients;

    public TodoApiClientContext(HttpClient http)
    {
        var registry = new ApiClientRegistry();

        var registrations = new IApiClientRegistration[]
        {
            new TodoTaskPageApiRegistration(),
            new TaskCommentsApiRegistration(),
            new TaskTagApiRegistration(),
            new TodoTaskApiRegistration(),
            new TodoListApiRegistration(),
            new TagToTaskApiRegistration(),
        };

        foreach (var registration in registrations)
        {
            registration.Register(registry, http);
        }

        this.apiClients = registry.ApiClients();
    }

    private ApiClient<T> GetApiClient<T>() where T : BaseEntity
    {
        if (this.apiClients.TryGetValue(typeof(T), out var client))
        {
            return (ApiClient<T>)client;
        }

        throw new NotSupportedException($"API client not configured for type {typeof(T).Name}");
    }

    public async Task<List<T>?> GetEntitiesAsync<T>() where T : BaseEntity =>
        await this.GetApiClient<T>().GetAllAsync();

    public async Task<T?> GetEntityAsync<T>(int id) where T : BaseEntity =>
        await this.GetApiClient<T>().GetAsync(id);

    public async Task SaveEntityAsync<T>(T todoList) where T : BaseEntity =>
        await this.GetApiClient<T>().SaveAsync(todoList);

    public async Task DeleteEntityAsync<T>(int id) where T : BaseEntity =>
        await this.GetApiClient<T>().DeleteAsync(id);
}
