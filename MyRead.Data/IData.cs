using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRead.Data
{
    public interface IData<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        
        Task<List<TEntity>> GetAllActiveAsync();
        Task<TEntity> GetByIdAsync(int entityId);


        void Remove(TEntity entity); //async?
        Task<int> CommitAsync();

    }
}
