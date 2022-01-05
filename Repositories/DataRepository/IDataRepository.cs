using CarDataRecognizer.Models;
using System;
using System.Linq;

namespace CarDataRecognizer.Repositories.AdatRepository
{
    public interface IDataRepository : IRepository<Adat>
    {
        IQueryable<Adat> GetByKameraIdAndDate(int kameraId, DateTime date);
        IQueryable<Adat> GetByPlateNumberAndDate(string plateNumber, DateTime date);

        /// <summary>
        /// Visszaadja a megadott dátumnál régebbi elemeket.
        /// </summary>
        /// <returns></returns>
        IQueryable<Adat> GetAllBeforeDateTime(DateTime date);
    }
}
