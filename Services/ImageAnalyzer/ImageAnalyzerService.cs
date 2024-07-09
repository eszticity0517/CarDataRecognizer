using IronOcr;

using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace CarDataRecognizer.Services.ImageAnalyzer;
public class ImageAnalyzerService : IImageAnalyzerService
{
    public async Task<string> ExtractImageDataFromStream(Stream stream)
    {
        Image image = Image.FromStream(stream);
        IronTesseract ocr = new() { };
        ocr.Configuration.ReadBarCodes = false;

        using OcrInput Input = new();
        Input.LoadImage(image);

        OcrResult result = await ocr.ReadAsync(Input);

        return result.Text;
    }
}

