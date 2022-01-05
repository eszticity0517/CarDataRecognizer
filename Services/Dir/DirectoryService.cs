using CarDataRecognizer.ConfigSections;
using CarDataRecognizer.Models;
using CarDataRecognizer.Services.ImageAnalyzer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CarDataRecognizer.Services.Dir
{
    public class DirectoryService : IDirectoryService
    {
        private readonly Config _config;
        private readonly ILogger _logger;
        private readonly IImageAnalyzerService _imageAnalyzerService;
        public DirectoryService(
            ILogger<DirectoryService> logger,
            IOptions<Config> config,
            IImageAnalyzerService imageAnalyzerService
        )
        {
            _config = config.Value;
            _logger = logger;
            _imageAnalyzerService = imageAnalyzerService;
        }

        public FileInfo[] ListFiles()
        {
            // D:\kepek
            DirectoryInfo directoryInfo = new DirectoryInfo(@"D:\kepek");

            FileInfo[] Images = directoryInfo.GetFiles("*.jpg");

            _logger.LogInformation("Images to process: " + Images.Length);
            return Images;
        }

        public async Task<string> GetBrand(FileInfo file)
        {
            using var stream = file.OpenRead();
            System.IO.BinaryReader textReader = new(stream);
            // JGP képeket 1000kb körül lehet maximalizálni.
            textReader.ReadBytes(10000000);
            // A képről levett adatokat hozzáadjuk.
            string brand = await _imageAnalyzerService.ExtractImageDataFromStream(stream);
            return brand;
        }
    }
}
