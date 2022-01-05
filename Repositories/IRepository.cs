using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDataRecognizer.Repositories
{
    public interface IRepository<TModel> where TModel : class
    {
        TModel GetById(int id);
        IEnumerable<TModel> GetAll();
        void Insert(TModel entity);
        Task InsertAsync(TModel entity);
        void Delete(int id);
        void Update(TModel entity);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
