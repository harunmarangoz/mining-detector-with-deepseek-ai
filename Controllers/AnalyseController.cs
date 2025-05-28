using Microsoft.AspNetCore.Mvc;
using MiningDetector.Models;
using MiningDetector.Services;

namespace MiningDetector.Controllers;

[ApiController]
[Route("api/ai-analyse")]
[Produces("application/json")]
public class AiAnalyseController(CryptoMinerAiAnalyzer cryptoMinerAiAnalyzer) : ControllerBase
{
    [HttpPost("file")]
    public async Task<ActionResult<AnalyzeResult>> AiAnalyzeFile(IFormFile file, [FromQuery] string? model = "deepseek-r1:7b")
    {
        try
        {
            if (file == null || file.Length == 0) return BadRequest("No file was uploaded.");
            
            var rawLogContent = await new StreamReader(file.OpenReadStream()).ReadToEndAsync();
            
            var result = await cryptoMinerAiAnalyzer.AnalyzeLog(rawLogContent, model);
            
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing the file.");
        }
    }
}