using IronOcr;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarDataRecognizer.Services.ImageAnalyzer
{
    public class ImageAnalyzerService : IImageAnalyzerService
    {
        public async Task<string> ExtractImageDataFromStream(Stream stream)
        {
            Image image = Image.FromStream(stream);
            IronTesseract OCR = new() { };
            OCR.Configuration.ReadBarCodes = false;

            using OcrInput Input = new(image);

            OcrResult result = await OCR.ReadAsync(Input);

            return result.Text;
        }
    }
}
