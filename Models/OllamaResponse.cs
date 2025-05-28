namespace MiningDetector.Models;

public class OllamaPromptResponse
{
    public string Model { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Response { get; set; }
    public bool Done { get; set; }
    public string DoneReason { get; set; }
    public long TotalDuration { get; set; }
    public int LoadDuration { get; set; }
    public int PromptEvalCount { get; set; }
    public long PromptEvalDuration { get; set; }
    public int EvalCount { get; set; }
    public int EvalDuration { get; set; }
}