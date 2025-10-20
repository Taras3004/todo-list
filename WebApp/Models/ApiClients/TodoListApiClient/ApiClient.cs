using WebApi.Entities.TodoDb;

namespace WebApp.Models.ApiClients.TodoListApiClient;

public class ApiClient<TEntity>
    where TEntity : BaseEntity
{
    private readonly HttpClient http;
    private readonly string apiRoute;

    public ApiClient(HttpClient http, string apiRoute)
    {
        this.http = http;
        this.apiRoute = apiRoute;
    }

    public async Task<List<TEntity>?> GetAllAsync()
    {
        return await this.http.GetFromJsonAsync<List<TEntity>>(this.apiRoute);
    }

    public async Task<TEntity?> GetAsync(int id)
    {
        return await this.http.GetFromJsonAsync<TEntity>($"{this.apiRoute}/{id}");
    }

    public async Task SaveAsync(TEntity entity)
    {
        if (entity.Id == 0)
        {
            await this.CreateAsync(entity);
        }
        else
        {
            await this.UpdateAsync(entity);
        }
    }

    public virtual async Task DeleteAsync(int id)
    {
        _ = await this.http.DeleteAsync($"{this.apiRoute}/{id}");
    }

    protected virtual async Task CreateAsync(TEntity entity)
    {
        var response = await this.http.PostAsJsonAsync(this.apiRoute, entity);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<TEntity>();
        if (created is not null)
        {
            var idProp = typeof(TEntity).GetProperty(nameof(created.Id));
            idProp?.SetValue(entity, idProp.GetValue(created));
        }
    }

    private async Task UpdateAsync(TEntity entity)
    {
        _ = await this.http.PutAsJsonAsync($"{this.apiRoute}/{entity.Id}", entity);
    }
}
