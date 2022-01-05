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

            // Optimised for details over speed.
            OCR.Language = OcrLanguage.LatinAlphabetBest;

            using OcrInput Input = new(image);
            Input.Binarize();
            Input.Contrast();
            Input.DeNoise();
            Input.Dilate();
            Input.Invert();
            Input.Sharpen();
            Input.ToGrayScale();

            OcrResult result = await OCR.ReadAsync(Input);

            return result.Text;
        }
    }
}
