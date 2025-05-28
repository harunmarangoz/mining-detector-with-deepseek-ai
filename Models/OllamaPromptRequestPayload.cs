namespace MiningDetector.Models;

public class OllamaPromptRequestPayload
{
    public string? Model { get; set; }
    public string System { get; set; }
    public OllamaPromptRequestPayloadFormat Format { get; set; }
    public string Prompt { get; set; }
    public bool Stream { get; set; } = false;
}

public class OllamaPromptRequestPayloadFormat
{
    public string Type { get; set; }
    public OllamaPromptRequestPayloadFormatProperties Properties { get; set; }
    public List<string> Required { get; set; }
}

public class OllamaPromptRequestPayloadFormatProperties
{
    public OllamaPromptRequestPayloadFormatPropertiesIsMining IsMining { get; set; }
}

public class OllamaPromptRequestPayloadFormatPropertiesIsMining
{
    public string Type { get; set; }
}