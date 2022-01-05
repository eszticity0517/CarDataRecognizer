using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarDataRecognizer.Services.ImageAnalyzer
{
    public interface IImageAnalyzerService
    {
        Task<string> ExtractImageDataFromStream(Stream stream);
    }
}
