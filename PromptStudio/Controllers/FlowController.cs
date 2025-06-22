using Microsoft.AspNetCore.Mvc;
using PromptStudio.Core.Domain;
using PromptStudio.Core.Interfaces;
using System.Text.Json;

namespace PromptStudio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlowController : ControllerBase
    {
        private readonly ILogger<FlowController> _logger;
        private readonly IFlowService _flowService;

        public FlowController(ILogger<FlowController> logger, IFlowService flowService)
        {
            _logger = logger;
            _flowService = flowService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromptFlow>>> GetFlows()
        {
            try
            {
                var flows = await _flowService.GetFlowsAsync();
                return Ok(flows);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving flows");
                return StatusCode(500, "An error occurred while retrieving flows");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PromptFlow>> GetFlow(Guid id)
        {
            try
            {
                var flow = await _flowService.GetFlowByIdAsync(id);
                if (flow == null)
                {
                    return NotFound();
                }
                return Ok(flow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving flow {FlowId}", id);
                return StatusCode(500, "An error occurred while retrieving the flow");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PromptFlow>> CreateFlow([FromBody] CreateFlowRequest request)
        {
            try
            {                var flow = new PromptFlow
                {
                    Name = request.Name,
                    Description = request.Description,
                    FlowData = JsonSerializer.Serialize(request.FlowData),
                    Tags = JsonSerializer.Serialize(request.Tags),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var createdFlow = await _flowService.CreateFlowAsync(flow);
                return CreatedAtAction(nameof(GetFlow), new { id = createdFlow.Id }, createdFlow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating flow");
                return StatusCode(500, "An error occurred while creating the flow");
            }
        }        [HttpPut("{id}")]
        public async Task<ActionResult<PromptFlow>> UpdateFlow(Guid id, [FromBody] UpdateFlowRequest request)
        {
            try
            {
                var existingFlow = await _flowService.GetFlowByIdAsync(id);
                if (existingFlow == null)
                {
                    return NotFound();
                }

                existingFlow.Name = request.Name;
                existingFlow.Description = request.Description;
                existingFlow.FlowData = JsonSerializer.Serialize(request.FlowData);
                existingFlow.Tags = JsonSerializer.Serialize(request.Tags);
                existingFlow.UpdatedAt = DateTime.UtcNow;

                var updatedFlow = await _flowService.UpdateFlowAsync(existingFlow);
                return Ok(updatedFlow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating flow {FlowId}", id);
                return StatusCode(500, "An error occurred while updating the flow");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlow(Guid id)
        {
            try
            {
                var flow = await _flowService.GetFlowByIdAsync(id);
                if (flow == null)
                {
                    return NotFound();
                }

                await _flowService.DeleteFlowAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting flow {FlowId}", id);
                return StatusCode(500, "An error occurred while deleting the flow");
            }
        }        [HttpPost("{id}/execute")]
        public async Task<ActionResult<FlowExecutionResult>> ExecuteFlow(Guid id, [FromBody] ExecuteFlowRequest request)
        {
            try
            {
                var flow = await _flowService.GetFlowByIdAsync(id);
                if (flow == null)
                {
                    return NotFound();
                }

                var result = await _flowService.ExecuteFlowAsync(id, request.Variables);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing flow {FlowId}", id);
                return StatusCode(500, "An error occurred while executing the flow");
            }
        }

        [HttpGet("{id}/executions")]
        public async Task<ActionResult<IEnumerable<FlowExecution>>> GetFlowExecutions(Guid id)
        {
            try
            {
                var executions = await _flowService.GetExecutionHistoryAsync(id);
                return Ok(executions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving flow executions for flow {FlowId}", id);
                return StatusCode(500, "An error occurred while retrieving flow executions");
            }
        }
    }

    public class CreateFlowRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public object FlowData { get; set; } = new { };
        public List<string> Tags { get; set; } = new();
    }

    public class UpdateFlowRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public object FlowData { get; set; } = new { };
        public List<string> Tags { get; set; } = new();
    }

    public class ExecuteFlowRequest
    {
        public Dictionary<string, object> Variables { get; set; } = new();
    }

    public class FlowExecutionResult
    {
        public int ExecutionId { get; set; }
        public Dictionary<string, object> Output { get; set; } = new();
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime ExecutedAt { get; set; }
    }
}
