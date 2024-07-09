using CarDataRecognizer.Models;

using System;
using System.Linq;

namespace CarDataRecognizer.Repositories.DataRepository;

public interface IDataRepository : IRepository<Data>
{
    /// <summary>
    /// Returns older element than the given date.
    /// </summary>
    /// <returns></returns>
    IQueryable<Data> GetAllBeforeDateTime(DateTime date);
}
