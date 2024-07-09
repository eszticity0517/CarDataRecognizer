namespace CarDataRecognizer.ConfigSections;

public class Config
{
    public required string CleanUnit { get; set; }
    public int CleanFrequency { get; set; }
    public int CleanInterval { get; set; }
    public required string DataProcessingUnit { get; set; }
    public int DataProcessingFrequency { get; set; }
}

