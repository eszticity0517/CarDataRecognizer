using System.IO;
using System.Threading.Tasks;

namespace CarDataRecognizer.Services.Dir;

public interface IDirectoryService
{
    FileInfo[] ListFiles();

    Task<string> GetBrand(FileInfo file);
}

