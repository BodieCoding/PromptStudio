using PromptStudio.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Diagnostics;

namespace PromptStudio.Core.Services;

/// <summary>
/// Model provider that integrates with MCP (Model Context Protocol) servers
/// </summary>
public class McpModelProvider : IModelProvider
{
    private readonly ILogger<McpModelProvider> _logger;
    private readonly HttpClient _httpClient;

    public string ProviderName => "mcp";

    public McpModelProvider(ILogger<McpModelProvider> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ModelInfo>> GetAvailableModelsAsync()
    {
        try
        {
            // Check if our MCP server is running and query available models
            // The MCP server should expose available models through its tools
            
            var models = new List<ModelInfo>();
            
            // Check if the local MCP server is available
            if (await IsMcpServerRunning())
            {
                models.Add(new ModelInfo
                {
                    Id = "mcp-claude",
                    Name = "Claude via MCP",
                    Description = "Claude model accessed through MCP server",
                    Provider = ProviderName,
                    Capabilities = new Dictionary<string, object>
                    {
                        ["maxTokens"] = 8192,
                        ["temperature"] = new { min = 0.0, max = 1.0, default_ = 0.7 },
                        ["supportsSystemMessage"] = true,
                        ["mcpTools"] = true
                    }
                });

                models.Add(new ModelInfo
                {
                    Id = "mcp-copilot",
                    Name = "Copilot via MCP",
                    Description = "GitHub Copilot accessed through MCP server",
                    Provider = ProviderName,
                    Capabilities = new Dictionary<string, object>
                    {
                        ["maxTokens"] = 4096,
                        ["temperature"] = new { min = 0.0, max = 1.0, default_ = 0.7 },
                        ["supportsSystemMessage"] = true,
                        ["mcpTools"] = true
                    }
                });
            }

            return models;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting available MCP models");
            return Enumerable.Empty<ModelInfo>();
        }
    }

    public async Task<ModelResponse> ExecutePromptAsync(ModelRequest request)
    {
        var startTime = DateTime.UtcNow;
        
        try
        {
            _logger.LogInformation("Executing prompt with MCP model {ModelId}", request.ModelId);

            // Check if MCP server is available
            if (!await IsMcpServerRunning())
            {
                return new ModelResponse
                {
                    Success = false,
                    ErrorMessage = "MCP server is not running. Please start the PromptStudio MCP server.",
                    ExecutionTimeMs = (long)(DateTime.UtcNow - startTime).TotalMilliseconds
                };
            }

            // Execute through MCP server
            var mcpResponse = await ExecuteViaMcpServer(request);
            
            return new ModelResponse
            {
                Success = mcpResponse.Success,
                Content = mcpResponse.Content,
                ErrorMessage = mcpResponse.ErrorMessage,
                ExecutionTimeMs = (long)(DateTime.UtcNow - startTime).TotalMilliseconds,
                TokensUsed = EstimateTokens(request.Prompt + (mcpResponse.Content ?? "")),
                Metadata = new Dictionary<string, object>
                {
                    ["model"] = request.ModelId,
                    ["provider"] = ProviderName,
                    ["mcpServer"] = true,
                    ["temperature"] = request.Parameters.GetValueOrDefault("temperature", 0.7)
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing prompt with MCP");
            
            return new ModelResponse
            {
                Success = false,
                ErrorMessage = ex.Message,
                ExecutionTimeMs = (long)(DateTime.UtcNow - startTime).TotalMilliseconds
            };
        }
    }

    public async Task<bool> IsAvailableAsync()
    {
        try
        {
            return await IsMcpServerRunning();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking MCP availability");
            return false;
        }
    }

    private async Task<bool> IsMcpServerRunning()
    {
        try
        {
            // Check if the MCP server process is running
            var mcpProcesses = Process.GetProcessesByName("PromptStudioMcpServer")
                .Concat(Process.GetProcessesByName("dotnet"))
                .Where(p => p.ProcessName.Contains("PromptStudio") || 
                           (p.MainModule?.FileName?.Contains("PromptStudioMcpServer") == true));

            if (mcpProcesses.Any())
            {
                _logger.LogDebug("Found MCP server process running");
                return true;
            }

            // Alternative: Check if MCP server is accessible via stdio/network
            // This would depend on how the MCP server is configured
            
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if MCP server is running");
            return false;
        }
    }

    private async Task<(bool Success, string? Content, string? ErrorMessage)> ExecuteViaMcpServer(ModelRequest request)
    {
        try
        {
            // For now, return a mock response
            // In a real implementation, this would:
            // 1. Connect to the MCP server (stdio, network, etc.)
            // 2. Send the prompt request using MCP protocol
            // 3. Parse the response and return structured data

            await Task.Delay(800); // Simulate MCP server communication

            var mockResponse = $"[MCP Response] Executed prompt via {request.ModelId}:\n\n" +
                             $"Your request has been processed through the Model Context Protocol server. " +
                             $"This response demonstrates how PromptStudio can integrate with MCP servers " +
                             $"to provide model-agnostic execution.\n\n" +
                             $"Variables provided: {string.Join(", ", request.Variables.Keys)}\n" +
                             $"Model parameters: {JsonSerializer.Serialize(request.Parameters)}";

            return (true, mockResponse, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing via MCP server");
            return (false, null, ex.Message);
        }
    }

    private int EstimateTokens(string text)
    {
        // Rough estimation: ~4 characters per token for English text
        return (int)Math.Ceiling(text.Length / 4.0);
    }
}
