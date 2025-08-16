using Microsoft.AspNetCore.Mvc;
using PromptStudio.Core.Interfaces;

namespace PromptStudio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelsController : ControllerBase
    {
        private readonly ILogger<ModelsController> _logger;
        private readonly IModelProviderManager _modelProviderManager;

        public ModelsController(ILogger<ModelsController> logger, IModelProviderManager modelProviderManager)
        {
            _logger = logger;
            _modelProviderManager = modelProviderManager;
        }

        /// <summary>
        /// Get all available models from all providers
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelInfo>>> GetAvailableModels()
        {
            try
            {
                var models = await _modelProviderManager.GetAllAvailableModelsAsync();
                return Ok(models);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving available models");
                return StatusCode(500, "An error occurred while retrieving available models");
            }
        }

        /// <summary>
        /// Get available providers
        /// </summary>
        [HttpGet("providers")]
        public ActionResult<IEnumerable<ProviderInfo>> GetProviders()
        {
            try
            {
                var providers = _modelProviderManager.GetProviders()
                    .Select(p => new ProviderInfo
                    {
                        Name = p.ProviderName,
                        IsAvailable = p.IsAvailableAsync().Result
                    });

                return Ok(providers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving providers");
                return StatusCode(500, "An error occurred while retrieving providers");
            }
        }

        /// <summary>
        /// Test a model by executing a simple prompt
        /// </summary>
        [HttpPost("test")]
        public async Task<ActionResult<ModelResponse>> TestModel([FromBody] TestModelRequest request)
        {
            try
            {
                var modelRequest = new ModelRequest
                {
                    ModelId = request.ModelId,
                    Prompt = request.Prompt ?? "Hello, please respond with 'Model test successful!'",
                    Parameters = request.Parameters ?? []
                };

                var response = await _modelProviderManager.ExecutePromptAsync(modelRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error testing model {ModelId}", request.ModelId);
                return StatusCode(500, "An error occurred while testing the model");
            }
        }
    }

    public class ProviderInfo
    {
        public string Name { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }

    public class TestModelRequest
    {
        public string ModelId { get; set; } = string.Empty;
        public string? Prompt { get; set; }
        public Dictionary<string, object>? Parameters { get; set; }
    }
}
