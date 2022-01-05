using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDataRecognizer.Repositories
{
    public abstract class Repository<TModel> : IDisposable, IRepository<TModel> where TModel : class
    {
        protected readonly DatabaseContext _dbContext;

        public Repository(DatabaseContext context)
        {
            _dbContext = context;
        }

        public void Delete(int id)
        {
            TModel model = _dbContext.Set<TModel>().Find(id);
            _dbContext.Set<TModel>().Remove(model);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TModel> GetAll()
        {
            return _dbContext.Set<TModel>().ToList();
        }

        public TModel GetById(int id)
        {
            return _dbContext.Set<TModel>().Find(id);
        }

        public void Insert(TModel entity)
        {
            _dbContext.Set<TModel>().Add(entity);
        }

        public void Update(TModel entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task InsertAsync(TModel entity)
        {
            await _dbContext.Set<TModel>().AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
