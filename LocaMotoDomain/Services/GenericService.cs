using LocaMoto.Domain.Interfaces.Repositories;
using LocaMoto.Domain.Interfaces.Services;

namespace LocaMoto.Domain.Services
{
    public class GenericService<TEntity>(IGenericRepository<TEntity> repositoryBase) : IGenericService<TEntity> where TEntity : class
    {        
        private readonly IGenericRepository<TEntity> _repositoryGeneric = repositoryBase;
        public async Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken) =>
            await _repositoryGeneric.AddRangeAsync(entities, cancellationToken);

        public async Task AddAsync(TEntity obj, CancellationToken cancellationToken) =>
            await _repositoryGeneric.AddAsync(obj, cancellationToken);

        public async Task UpdateAsync(TEntity obj, CancellationToken cancellationToken) =>
            await _repositoryGeneric.UpdateAsync(obj, cancellationToken);

        public async Task UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken) =>
            await _repositoryGeneric.UpdateRangeAsync(entities, cancellationToken);

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken) =>        
            await _repositoryGeneric.GetAllAsync(cancellationToken);
        
        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            await _repositoryGeneric.GetByIdAsync(id, cancellationToken);

        public async Task DeleteAsync(TEntity obj, CancellationToken cancellationToken) =>
            await _repositoryGeneric.DeleteAsync(obj, cancellationToken);

        public async Task DeleteAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken) =>
            await _repositoryGeneric.DeleteAllAsync(entities, cancellationToken);
    }
}
