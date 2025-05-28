using System.Text;
using System.Text.Json;
using MiningDetector.Models;

namespace MiningDetector.Services;

public class CryptoMinerAiAnalyzer
{
    public async Task<AnalyzeResult> AnalyzeLog(string logContent, string? model = "deepseek-r1:7b")
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:11434");
        httpClient.Timeout = new TimeSpan(1, 0, 0);

        var requestPayload = new OllamaPromptRequestPayload()
        {
            Model = model,
            Stream = false,
            System =
                "You are a code and log analysis expert. You will be given a log file or a script as input. Your task is to determine whether the content is related to cryptocurrency mining activity. Look for signs such as references to mining software (e.g., xmrig, minerd), wallet addresses, mining pools, unusual CPU or GPU usage patterns, or any command-line instructions typically used for crypto mining.\n\nBased on your analysis, respond ONLY with a valid JSON object in one of the following formats:\n\nIf cryptocurrency mining is detected:\n{\"isMining\": true}\n\nIf cryptocurrency mining is not detected:\n{\"isMining\": false}\n\nDo not include any explanation, markdown formatting, code blocks, or additional text. Return only the raw JSON output.",
            Format = new()
            {
                Type = "object",
                Properties = new()
                {
                    IsMining = new()
                    {
                        Type = "boolean"
                    }
                },
                Required = ["isMining"],
            },
            Prompt = logContent
        };

        var requestPayloadStr = JsonSerializer.Serialize(requestPayload, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var content = new StringContent(requestPayloadStr, Encoding.UTF8, "application/json");

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var response = await httpClient.PostAsync("/api/generate", content);
        response.EnsureSuccessStatusCode();
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed.ToString("g"));
        
        var responseStr = await response.Content.ReadAsStringAsync();
        var ollamaPromptResponse = JsonSerializer.Deserialize<OllamaPromptResponse>(responseStr,
            new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

        if (ollamaPromptResponse == null || string.IsNullOrWhiteSpace(ollamaPromptResponse.Response))
        {
            throw new InvalidOperationException("Received an empty or invalid response from the AI service.");
        }

        var analyzeResult = JsonSerializer.Deserialize<AnalyzeResult>(ollamaPromptResponse.Response, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return analyzeResult;
    }
}
