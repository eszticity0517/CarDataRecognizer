using System.IO;
using System.Threading.Tasks;

namespace CarDataRecognizer.Services.ImageAnalyzer;
public interface IImageAnalyzerService
{
    Task<string> ExtractImageDataFromStream(Stream stream);
}
