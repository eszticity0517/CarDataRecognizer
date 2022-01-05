using CarDataRecognizer.Models;
using System;
using System.Linq;

namespace CarDataRecognizer.Repositories.AdatRepository
{
    public class DataRepository : Repository<Data>, IDataRepository
    {
        public DatabaseContext DatabaseContext
        {
            get { return _dbContext; }
        }

        public DataRepository(DatabaseContext context) : base(context) { }

        public IQueryable<Data> GetAllBeforeDateTime(DateTime date)
        {
            return _dbContext.Adatok
                .Where(adat => adat.Date < date);
        }
    }
}
