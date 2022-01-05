using CarDataRecognizer.Models;
using System;
using System.Linq;

namespace CarDataRecognizer.Repositories.AdatRepository
{
    public class DataRepository : Repository<Adat>, IDataRepository
    {
        public DatabaseContext DatabaseContext
        {
            get { return _dbContext; }
        }

        public DataRepository(DatabaseContext context) : base(context) { }

        public IQueryable<Adat> GetByKameraIdAndDate(int kameraId, DateTime date)
        {
            return _dbContext.Adatok
                .Where(adat => adat.KameraId == kameraId && adat.Date > date)
                .OrderByDescending(adat => adat.Date);
        }

        public IQueryable<Adat> GetByPlateNumberAndDate(string plateNumber, DateTime date)
        {
            return _dbContext.Adatok
              .Where(adat => adat.PlateNumber == plateNumber && adat.Date == date);
        }

        public IQueryable<Adat> GetAllBeforeDateTime(DateTime date)
        {
            return _dbContext.Adatok
                .Where(adat => adat.Date < date);
        }
    }
}
