using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public abstract class AbstractEntityController<TEntity> : ControllerBase
    where TEntity : BaseEntity
{
    private readonly DbContext dbContext;
    private readonly DbSet<TEntity> dbSet;

    private string CurrentUserId => this.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    protected AbstractEntityController(DbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<TEntity>();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TEntity entity)
    {
        if (entity is IUserOwnedEntity userOwnedEntity)
        {
            userOwnedEntity.UserId = this.CurrentUserId;
        }

        _ = await this.dbSet.AddAsync(entity);
        _ = await this.dbContext.SaveChangesAsync();

        return this.Ok(entity);
    }

    [HttpGet("{entityId}")]
    public async Task<IActionResult> Read(int entityId)
    {
        var entity = await this.dbSet.FindAsync(entityId);

        if (entity == null)
        {
            _ = this.NotFound("Entity doesn't exist");
        }

        if (entity is IUserOwnedEntity userOwnedEntity && userOwnedEntity.UserId != this.CurrentUserId)
        {
            return this.Forbid();
        }

        return this.Ok(entity);
    }

    [HttpPut("{entityId}")]
    public async Task<IActionResult> Update(int entityId, [FromBody] TEntity entity)
    {
        if (entityId != entity.Id)
        {
            return this.BadRequest("ID in URL must match ID in body.");
        }

        var existingEntity = await this.dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == entityId);
        if (existingEntity == null)
        {
            return this.NotFound("Entity doesn't exist");
        }

        if (existingEntity is IUserOwnedEntity userOwnedEntity && userOwnedEntity.UserId != this.CurrentUserId)
        {
            return this.Forbid();
        }

        if (entity is IUserOwnedEntity updatedUserOwnedEntity)
        {
            updatedUserOwnedEntity.UserId = this.CurrentUserId;
        }

        this.dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
        _ = await this.dbContext.SaveChangesAsync();
        return this.Ok();
    }

    [HttpDelete("{entityId}")]
    public async Task<IActionResult> Delete(int entityId)
    {
        var entity = await this.dbSet.FirstOrDefaultAsync(x => x.Id == entityId);

        if (entity == null)
        {
            return this.NotFound();
        }

        if (entity is IUserOwnedEntity userOwnedEntity && userOwnedEntity.UserId != this.CurrentUserId)
        {
            return this.Forbid();
        }

        _ = this.dbSet.Remove(entity);
        _ = await this.dbContext.SaveChangesAsync();
        return this.Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = this.dbSet.AsQueryable();

        if (typeof(IUserOwnedEntity).IsAssignableFrom(typeof(TEntity)))
        {
            query = query.Where(e => ((IUserOwnedEntity)e).UserId == this.CurrentUserId);
        }

        return this.Ok(await query.ToListAsync());
    }
}
