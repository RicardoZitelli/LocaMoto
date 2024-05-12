namespace LocaMotoDomain.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}