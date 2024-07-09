using CarDataRecognizer.ConfigSections;
using CarDataRecognizer.Services.ImageAnalyzer;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.IO;
using System.Threading.Tasks;

namespace CarDataRecognizer.Services.Dir;

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
        // Might be nice to make it configurable from appsettings.json.
        DirectoryInfo directoryInfo = new DirectoryInfo(@"D:\kepek");

        FileInfo[] Images = directoryInfo.GetFiles("*.jpg");

        _logger.LogInformation("Images to process: " + Images.Length);
        return Images;
    }

    public async Task<string> GetBrand(FileInfo file)
    {
        using FileStream stream = file.OpenRead();
        BinaryReader textReader = new(stream);
        // Let's estimate a maximum size of 1000 megabytes.
        textReader.ReadBytes(10000000);
        
        string brand = await _imageAnalyzerService.ExtractImageDataFromStream(stream);
        return brand;
    }
}

