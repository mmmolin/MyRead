using MyRead.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyRead.Data
{
    public interface IData<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int entityId);


        //void DeleteBook(int bookId);
        Task<int> CommitAsync();

    }
}
