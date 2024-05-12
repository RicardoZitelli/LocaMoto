namespace LocaMotoDomain.Interfaces.Services
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity obj, CancellationToken cancellationToken);
        Task AddRangeAsync(List<TEntity> list, CancellationToken cancellationToken);
        Task UpdateAsync(TEntity obj, CancellationToken cancellationToken);
        Task UpdateRangeAsync(List<TEntity> list, CancellationToken cancellationToken);
        Task DeleteAsync(TEntity obj, CancellationToken cancellationToken);
        Task DeleteAllAsync(IEnumerable<TEntity> list, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
