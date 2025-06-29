using PromptStudio.Core.Interfaces;
using PromptStudio.Core.Domain;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PromptStudio.Core.Services;

/// <summary>
/// Service for managing visual prompt flows
/// </summary>
public class FlowService : IFlowService
{
    private readonly ILogger<FlowService> _logger;
    private readonly IModelProviderManager _modelProviderManager;
    
    // TODO: Add IPromptStudioDbContext when database schema is ready
    // private readonly IPromptStudioDbContext _context;

    public FlowService(ILogger<FlowService> logger, IModelProviderManager modelProviderManager)
    {
        _logger = logger;
        _modelProviderManager = modelProviderManager;
    }

    /// <summary>
    /// Get all flows for a user with optional filtering
    /// </summary>
    public async Task<IEnumerable<PromptFlow>> GetFlowsAsync(string? userId = null, string? tag = null, string? search = null)
    {
        try
        {
            // TODO: Implement database query
            // For now, return mock data for development
            await Task.Delay(100); // Simulate async operation

            var mockFlows = new List<PromptFlow>
            {
                new PromptFlow
                {
                    Id = Guid.NewGuid(),
                    Name = "Sample Flow 1",
                    Description = "A sample prompt flow for testing",
                    Version = "1.0.0",
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    UpdatedAt = DateTime.UtcNow.AddDays(-1),
                    Tags = JsonSerializer.Serialize(new[] { "test", "sample" }),
                    FlowData = JsonSerializer.Serialize(CreateSampleFlowData()),
                    IsActive = true
                },
                new PromptFlow
                {
                    Id = Guid.NewGuid(),
                    Name = "Customer Support Flow",
                    Description = "Automated customer support response flow",
                    Version = "2.1.0",
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    UpdatedAt = DateTime.UtcNow.AddHours(-2),
                    Tags = JsonSerializer.Serialize(new[] { "production", "customer-support" }),
                    FlowData = JsonSerializer.Serialize(CreateComplexFlowData()),
                    IsActive = true
                }
            };

            // Apply filters if provided
            var filteredFlows = mockFlows.AsEnumerable();
            
            if (!string.IsNullOrEmpty(tag))
            {
                filteredFlows = filteredFlows.Where(f => f.Tags?.Contains(tag) == true);
            }
            
            if (!string.IsNullOrEmpty(search))
            {
                filteredFlows = filteredFlows.Where(f => 
                    f.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (f.Description?.Contains(search, StringComparison.OrdinalIgnoreCase) == true));
            }

            return filteredFlows.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving flows");
            throw;
        }
    }

    /// <summary>
    /// Get a specific flow by ID
    /// </summary>
    public async Task<PromptFlow?> GetFlowByIdAsync(Guid flowId)
    {
        try
        {
            // TODO: Implement database query
            await Task.Delay(50); // Simulate async operation

            // Return mock flow for development
            return new PromptFlow
            {
                Id = flowId,
                Name = "Sample Flow",
                Description = "A sample prompt flow",
                Version = "1.0.0",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow.AddDays(-1),
                Tags = JsonSerializer.Serialize(new[] { "test", "sample" }),
                FlowData = JsonSerializer.Serialize(CreateSampleFlowData()),
                IsActive = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving flow {FlowId}", flowId);
            throw;
        }
    }

    /// <summary>
    /// Create a new prompt flow
    /// </summary>
    public async Task<PromptFlow> CreateFlowAsync(PromptFlow flow)
    {
        try
        {
            // TODO: Implement database save
            flow.Id = Guid.NewGuid();
            flow.CreatedAt = DateTime.UtcNow;
            flow.UpdatedAt = DateTime.UtcNow;
            flow.Version = "1.0.0";
            flow.IsActive = true;

            await Task.Delay(100); // Simulate async operation

            _logger.LogInformation("Created new flow {FlowId} with name '{FlowName}'", flow.Id, flow.Name);

            return flow;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating flow");
            throw;
        }
    }

    /// <summary>
    /// Update an existing prompt flow
    /// </summary>
    public async Task<PromptFlow> UpdateFlowAsync(PromptFlow flow)
    {
        try
        {
            // TODO: Implement database update
            flow.UpdatedAt = DateTime.UtcNow;
            
            // Increment patch version
            var versionParts = flow.Version.Split('.');
            if (versionParts.Length >= 3 && int.TryParse(versionParts[2], out int patch))
            {
                flow.Version = $"{versionParts[0]}.{versionParts[1]}.{patch + 1}";
            }

            await Task.Delay(100); // Simulate async operation

            _logger.LogInformation("Updated flow {FlowId}", flow.Id);

            return flow;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating flow {FlowId}", flow.Id);
            throw;
        }
    }

    /// <summary>
    /// Delete a prompt flow
    /// </summary>
    public async Task<bool> DeleteFlowAsync(Guid flowId)
    {
        try
        {
            // TODO: Implement database deletion
            await Task.Delay(50); // Simulate async operation

            _logger.LogInformation("Deleted flow {FlowId}", flowId);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting flow {FlowId}", flowId);
            throw;
        }
    }    /// <summary>
    /// Execute a prompt flow with given variables
    /// </summary>
    public async Task<FlowExecutionResult> ExecuteFlowAsync(Guid flowId, Dictionary<string, object> variables, FlowExecutionOptions? options = null)
    {
        var startTime = DateTime.UtcNow;
        var executionId = Guid.NewGuid();
        
        try
        {
            _logger.LogInformation("Starting execution of flow {FlowId} with execution ID {ExecutionId}", flowId, executionId);

            // Get the flow
            var flow = await GetFlowByIdAsync(flowId);
            if (flow == null)
            {                return new FlowExecutionResult
                {
                    Success = false,
                    ExecutionId = executionId,
                    Error = "Flow not found",
                    ExecutionTime = (long)(DateTime.UtcNow - startTime).TotalMilliseconds,
                    NodeExecutions = new List<NodeExecutionInfo>()
                };
            }

            // Parse flow data
            var flowData = JsonSerializer.Deserialize<Dictionary<string, object>>(flow.FlowData);
            var nodes = ExtractNodesFromFlowData(flowData);
            var edges = ExtractEdgesFromFlowData(flowData);

            // Execute the flow
            var result = await ExecuteFlowNodes(nodes, edges, variables, executionId);
            result.ExecutionId = executionId;

            _logger.LogInformation("Completed execution of flow {FlowId} in {ExecutionTime}ms", 
                flowId, result.ExecutionTime);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing flow {FlowId}", flowId);
            
            return new FlowExecutionResult
            {
                Success = false,
                ExecutionId = executionId,
                Error = ex.Message,
                ExecutionTime = (long)(DateTime.UtcNow - startTime).TotalMilliseconds,
                NodeExecutions = new List<NodeExecutionInfo>()
            };
        }
    }

    private async Task<FlowExecutionResult> ExecuteFlowNodes(
        List<FlowNodeInfo> nodes, 
        List<FlowEdgeInfo> edges, 
        Dictionary<string, object> variables, 
        Guid executionId)
    {
        var startTime = DateTime.UtcNow;
        var nodeExecutions = new List<NodeExecutionInfo>();
        var flowVariables = new Dictionary<string, object>(variables);
        
        try
        {
            // Find starting nodes (nodes with no incoming edges)
            var startingNodes = nodes.Where(n => !edges.Any(e => e.Target == n.Id)).ToList();
            
            if (!startingNodes.Any())
            {
                startingNodes = nodes.Take(1).ToList(); // Fallback to first node
            }

            // Execute nodes in topological order
            var executedNodes = new HashSet<string>();
            var nodesToExecute = new Queue<FlowNodeInfo>(startingNodes);
            
            while (nodesToExecute.Count > 0)
            {
                var currentNode = nodesToExecute.Dequeue();
                
                if (executedNodes.Contains(currentNode.Id))
                    continue;                var nodeExecution = await ExecuteNode(currentNode, flowVariables);
                var nodeStatus = ConvertToNodeExecutionInfo(nodeExecution);
                nodeExecutions.Add(nodeStatus);
                executedNodes.Add(currentNode.Id);

                // Update flow variables with node output
                if (nodeExecution.OutputData != null && nodeExecution.OutputData != "{}")
                {
                    // Parse output data and extract output value
                    var outputData = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(nodeExecution.OutputData);
                    if (outputData?.ContainsKey("output") == true)
                    {
                        flowVariables[$"node_{currentNode.Id}_output"] = outputData["output"];
                    }
                }

                // Find next nodes to execute
                var outgoingEdges = edges.Where(e => e.Source == currentNode.Id);
                foreach (var edge in outgoingEdges)
                {
                    var nextNode = nodes.FirstOrDefault(n => n.Id == edge.Target);
                    if (nextNode != null && !executedNodes.Contains(nextNode.Id))
                    {
                        // Check if all incoming nodes have been executed
                        var incomingEdges = edges.Where(e => e.Target == nextNode.Id);
                        var allPredecessorsExecuted = incomingEdges.All(e => executedNodes.Contains(e.Source));
                        
                        if (allPredecessorsExecuted)
                        {
                            nodesToExecute.Enqueue(nextNode);
                        }
                    }
                }
            }            // Determine final output
            var outputNode = nodeExecutions.LastOrDefault(ne => ne.Status == "completed");
            var finalOutput = outputNode?.Output ?? "Flow completed successfully";

            return new FlowExecutionResult
            {
                Success = true,
                Output = finalOutput,
                ExecutionTime = (long)(DateTime.UtcNow - startTime).TotalMilliseconds,
                NodeExecutions = nodeExecutions
            };
        }
        catch (Exception ex)
        {
            return new FlowExecutionResult
            {
                Success = false,
                Error = ex.Message,
                ExecutionTime = (long)(DateTime.UtcNow - startTime).TotalMilliseconds,
                NodeExecutions = nodeExecutions
            };
        }
    }    private async Task<NodeExecution> ExecuteNode(FlowNodeInfo node, Dictionary<string, object> variables)
    {
        var startTime = DateTime.UtcNow;
        var nodeExecution = new NodeExecution
        {
            NodeKey = node.Id,
            NodeType = ParseNodeType(node.Type),
            StartTime = startTime,
            Status = NodeExecutionStatus.Running,
            InputData = JsonSerializer.Serialize(variables)
        };

        try
        {
            object? output = null;

            switch (node.Type?.ToLowerInvariant())
            {
                case "prompt":
                    output = await ExecutePromptNode(node, variables);
                    break;

                case "variable":
                    output = ExecuteVariableNode(node, variables);
                    break;

                case "conditional":
                    output = ExecuteConditionalNode(node, variables);
                    break;

                case "transform":
                    output = ExecuteTransformNode(node, variables);
                    break;

                case "output":
                    output = ExecuteOutputNode(node, variables);
                    break;

                default:
                    output = $"Unsupported node type: {node.Type}";
                    break;
            }

            nodeExecution.EndTime = DateTime.UtcNow;
            nodeExecution.Status = NodeExecutionStatus.Completed;
            nodeExecution.OutputData = JsonSerializer.Serialize(new { output = output });
            nodeExecution.ExecutionTimeMs = (int)(nodeExecution.EndTime.Value - nodeExecution.StartTime).TotalMilliseconds;
        }
        catch (Exception ex)
        {
            nodeExecution.EndTime = DateTime.UtcNow;
            nodeExecution.Status = NodeExecutionStatus.Failed;
            nodeExecution.ErrorMessage = ex.Message;
            nodeExecution.ExecutionTimeMs = (int)(nodeExecution.EndTime.Value - nodeExecution.StartTime).TotalMilliseconds;
            _logger.LogError(ex, "Error executing node {NodeId}", node.Id);
        }

        return nodeExecution;
    }

    private FlowNodeType ParseNodeType(string? nodeType)
    {
        return nodeType?.ToLowerInvariant() switch
        {
            "input" => FlowNodeType.Input,
            "prompt" => FlowNodeType.Prompt,
            "variable" => FlowNodeType.Variable,
            "conditional" => FlowNodeType.Conditional,
            "transform" => FlowNodeType.Transform,
            "output" => FlowNodeType.Output,
            "llmcall" => FlowNodeType.LlmCall,
            "template" => FlowNodeType.Template,
            "loop" => FlowNodeType.Loop,
            "parallel" => FlowNodeType.Parallel,
            "apicall" => FlowNodeType.ApiCall,
            "validation" => FlowNodeType.Validation,
            "aggregation" => FlowNodeType.Aggregation,
            _ => FlowNodeType.Prompt
        };
    }

    private NodeExecutionInfo ConvertToNodeExecutionInfo(NodeExecution nodeExecution)
    {
        var outputData = new Dictionary<string, object>();
        if (!string.IsNullOrEmpty(nodeExecution.OutputData) && nodeExecution.OutputData != "{}")
        {
            try
            {
                outputData = JsonSerializer.Deserialize<Dictionary<string, object>>(nodeExecution.OutputData) ?? new();
            }
            catch
            {
                // If deserialization fails, treat as simple string
                outputData["output"] = nodeExecution.OutputData;
            }
        }

        var inputData = new Dictionary<string, object>();
        if (!string.IsNullOrEmpty(nodeExecution.InputData) && nodeExecution.InputData != "{}")
        {
            try
            {
                inputData = JsonSerializer.Deserialize<Dictionary<string, object>>(nodeExecution.InputData) ?? new();
            }
            catch
            {
                inputData["input"] = nodeExecution.InputData;
            }
        }

        return new NodeExecutionInfo
        {
            NodeId = nodeExecution.NodeKey,
            StartTime = nodeExecution.StartTime,
            EndTime = nodeExecution.EndTime,
            Input = inputData.Count > 0 ? inputData : null,
            Output = outputData.ContainsKey("output") ? outputData["output"] : null,
            Status = nodeExecution.Status.ToString().ToLowerInvariant(),
            Error = nodeExecution.ErrorMessage
        };
    }

    private async Task<object> ExecutePromptNode(FlowNodeInfo node, Dictionary<string, object> variables)
    {
        try
        {
            var data = node.Data;            var content = data.TryGetValue("content", out var contentObj) ? contentObj?.ToString() ?? "" : "";
            var model = data.TryGetValue("model", out var modelObj) ? modelObj?.ToString() ?? "gpt-3.5-turbo" : "gpt-3.5-turbo";
            var systemMessage = data.TryGetValue("systemMessage", out var sysObj) ? sysObj?.ToString() : null;
            
            // Extract parameters
            var parameters = new Dictionary<string, object>();
            if (data.ContainsKey("parameters") && data["parameters"] is JsonElement paramsElement)
            {
                if (paramsElement.ValueKind == JsonValueKind.Object)
                {
                    foreach (var prop in paramsElement.EnumerateObject())
                    {
                        parameters[prop.Name] = prop.Value.GetRawText();
                    }
                }
            }

            // Replace variables in content
            var processedContent = ReplaceVariables(content!, variables);
            var processedSystemMessage = !string.IsNullOrEmpty(systemMessage) 
                ? ReplaceVariables(systemMessage, variables) 
                : null;            // Execute via model provider
            var request = new ModelRequest
            {
                ModelId = model!,
                Prompt = processedContent,
                SystemMessage = processedSystemMessage,
                Parameters = parameters,
                Variables = variables
            };

            var response = await _modelProviderManager.ExecutePromptAsync(request);
            
            if (response.Success)
            {
                return response.Content ?? "No content returned";
            }
            else
            {
                throw new Exception($"Model execution failed: {response.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing prompt node {NodeId}", node.Id);
            throw;
        }
    }

    private object ExecuteVariableNode(FlowNodeInfo node, Dictionary<string, object> variables)
    {
        var data = node.Data;        var varName = data.TryGetValue("name", out var nameObj) ? nameObj?.ToString() ?? "" : "";
        var defaultValue = data.TryGetValue("defaultValue", out var defaultObj) ? defaultObj : null;

        if (!string.IsNullOrEmpty(varName) && variables.ContainsKey(varName!))
        {
            return variables[varName!];
        }

        return defaultValue ?? $"Variable '{varName}' not found";
    }

    private object ExecuteConditionalNode(FlowNodeInfo node, Dictionary<string, object> variables)
    {
        // TODO: Implement conditional logic based on node.Data["condition"]
        return "Conditional node executed (not implemented)";
    }

    private object ExecuteTransformNode(FlowNodeInfo node, Dictionary<string, object> variables)
    {
        // TODO: Implement data transformation based on node.Data["transformType"]
        return "Transform node executed (not implemented)";
    }

    private object ExecuteOutputNode(FlowNodeInfo node, Dictionary<string, object> variables)
    {
        var data = node.Data;
        var template = data.TryGetValue("template", out var templateObj) ? templateObj?.ToString() ?? "" : "";
        
        if (!string.IsNullOrEmpty(template))
        {
            return ReplaceVariables(template!, variables);
        }

        return variables;
    }

    private string ReplaceVariables(string content, Dictionary<string, object> variables)
    {
        var result = content;
        
        // Replace {{variableName}} patterns
        var regex = new Regex(@"\{\{([^}]+)\}\}");
        result = regex.Replace(result, match =>
        {
            var varName = match.Groups[1].Value.Trim();
            if (variables.ContainsKey(varName))
            {
                return variables[varName]?.ToString() ?? "";
            }
            return match.Value; // Keep original if variable not found
        });

        return result;
    }

    private List<FlowNodeInfo> ExtractNodesFromFlowData(Dictionary<string, object>? flowData)
    {
        var nodes = new List<FlowNodeInfo>();
        
        if (flowData?.ContainsKey("nodes") == true && flowData["nodes"] is JsonElement nodesElement)
        {
            if (nodesElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var nodeElement in nodesElement.EnumerateArray())
                {
                    var node = new FlowNodeInfo
                    {
                        Id = GetJsonProperty(nodeElement, "id"),
                        Type = GetJsonProperty(nodeElement, "type"),
                        Data = ExtractNodeData(nodeElement)
                    };
                    nodes.Add(node);
                }
            }
        }

        return nodes;
    }

    private List<FlowEdgeInfo> ExtractEdgesFromFlowData(Dictionary<string, object>? flowData)
    {
        var edges = new List<FlowEdgeInfo>();
        
        if (flowData?.ContainsKey("edges") == true && flowData["edges"] is JsonElement edgesElement)
        {
            if (edgesElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var edgeElement in edgesElement.EnumerateArray())
                {
                    var edge = new FlowEdgeInfo
                    {
                        Id = GetJsonProperty(edgeElement, "id"),
                        Source = GetJsonProperty(edgeElement, "source"),
                        Target = GetJsonProperty(edgeElement, "target")
                    };
                    edges.Add(edge);
                }
            }
        }

        return edges;
    }

    private Dictionary<string, object> ExtractNodeData(JsonElement nodeElement)
    {
        var data = new Dictionary<string, object>();
        
        if (nodeElement.TryGetProperty("data", out var dataElement))
        {
            if (dataElement.ValueKind == JsonValueKind.Object)
            {
                foreach (var prop in dataElement.EnumerateObject())
                {
                    data[prop.Name] = prop.Value;
                }
            }
        }

        return data;
    }

    private string GetJsonProperty(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out var prop))
        {
            return prop.GetString() ?? "";
        }
        return "";
    }

    // Helper classes for flow execution
    private class FlowNodeInfo
    {
        public string Id { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public Dictionary<string, object> Data { get; set; } = new();
    }

    private class FlowEdgeInfo
    {
        public string Id { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Target { get; set; } = string.Empty;
    }

    /// <summary>
    /// Validate a prompt flow structure
    /// </summary>
    public async Task<FlowValidationResult> ValidateFlowAsync(PromptFlow flow)
    {
        try
        {
            // TODO: Implement comprehensive flow validation
            await Task.Delay(200); // Simulate validation time

            var result = new FlowValidationResult
            {
                IsValid = true,
                Errors = new List<ValidationError>(),
                Warnings = new List<ValidationWarning>
                {
                    new ValidationWarning
                    {
                        NodeId = "node-2",
                        Message = "Consider reducing temperature for more consistent outputs",
                        Type = "optimization"
                    }
                }
            };

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating flow {FlowId}", flow.Id);
            throw;
        }
    }

    /// <summary>
    /// Get execution history for a flow
    /// </summary>
    public async Task<IEnumerable<FlowExecution>> GetExecutionHistoryAsync(Guid flowId, int limit = 50)
    {
        try
        {
            // TODO: Implement database query
            await Task.Delay(100); // Simulate async operation            // Return mock execution history
            var mockHistory = new List<FlowExecution>
            {
                new FlowExecution
                {
                    Id = Guid.NewGuid(),
                    FlowId = flowId,
                    FlowVersion = "1.0.0",
                    InputVariables = JsonSerializer.Serialize(new { user_input = "Hello, world!" }),
                    OutputResult = JsonSerializer.Serialize("Hello! How can I help you today?"),
                    TotalExecutionTime = 1250,
                    Status = FlowExecutionStatus.Completed,
                    CreatedAt = DateTime.UtcNow.AddHours(-1)
                },
                new FlowExecution
                {
                    Id = Guid.NewGuid(),
                    FlowId = flowId,
                    FlowVersion = "1.0.0",
                    InputVariables = JsonSerializer.Serialize(new { user_input = "What's the weather?" }),
                    OutputResult = JsonSerializer.Serialize("I don't have access to real-time weather data."),
                    TotalExecutionTime = 980,
                    Status = FlowExecutionStatus.Completed,
                    CreatedAt = DateTime.UtcNow.AddHours(-2)
                }
            };

            return mockHistory.Take(limit);
        }        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving execution history for flow {FlowId}", flowId);
            throw;
        }
    }

    #region Private Helper Methods
    
    private static object CreateSampleFlowData()
    {
        return new
        {
            Variables = new { },
            Nodes = new object[]
            {
                new
                {
                    Id = "node-1",
                    Type = "variable",
                    Position = new { X = 100, Y = 100 },
                    Data = new
                    {
                        Label = "Input Variable",
                        Name = "user_input",
                        Type = "string",
                        DefaultValue = "",
                        Description = "User input for the prompt"
                    }
                },
                new
                {
                    Id = "node-2",
                    Type = "prompt",
                    Position = new { X = 300, Y = 100 },
                    Data = new
                    {
                        Label = "Main Prompt",
                        Content = "Please respond to: {{user_input}}",
                        Model = "gpt-4",
                        Parameters = new
                        {
                            Temperature = 0.7,
                            MaxTokens = 1000,
                            TopP = 1.0
                        },
                        Variables = new[] { new { Name = "user_input" } }
                    }
                },
                new
                {
                    Id = "node-3",
                    Type = "output",
                    Position = new { X = 500, Y = 100 },
                    Data = new
                    {
                        Label = "Response Output",
                        Format = "text",
                        Description = "Final response to user"
                    }
                }
            },
            Edges = new object[]
            {
                new
                {
                    Id = "edge-1",
                    Source = "node-1",
                    Target = "node-2",
                    Type = "smoothstep"
                },
                new
                {
                    Id = "edge-2",
                    Source = "node-2",
                    Target = "node-3",
                    Type = "smoothstep"
                }
            }
        };
    }

    private static object CreateComplexFlowData()
    {
        return new
        {
            Variables = new { },
            Nodes = new object[]
            {
                new
                {
                    Id = "node-1",
                    Type = "variable",
                    Position = new { X = 50, Y = 100 },
                    Data = new
                    {
                        Label = "Customer Query",
                        Name = "customer_query",
                        Type = "string",
                        Description = "The customer's question or issue"
                    }
                },
                new
                {
                    Id = "node-2",
                    Type = "conditional",
                    Position = new { X = 250, Y = 100 },
                    Data = new
                    {
                        Label = "Query Type Check",
                        Condition = new
                        {
                            LeftOperand = "customer_query",
                            Operator = "contains",
                            RightOperand = "refund"
                        }
                    }
                },
                new
                {
                    Id = "node-3",
                    Type = "prompt",
                    Position = new { X = 150, Y = 250 },
                    Data = new
                    {
                        Label = "Refund Response",
                        Content = "Customer is asking about a refund: {{customer_query}}. Provide refund policy information.",
                        Model = "gpt-4"
                    }
                },
                new
                {
                    Id = "node-4",
                    Type = "prompt",
                    Position = new { X = 350, Y = 250 },
                    Data = new
                    {
                        Label = "General Response",
                        Content = "Customer query: {{customer_query}}. Provide helpful assistance.",
                        Model = "gpt-4"
                    }
                },
                new
                {
                    Id = "node-5",
                    Type = "output",
                    Position = new { X = 250, Y = 400 },
                    Data = new
                    {
                        Label = "Final Response",
                        Format = "text"
                    }
                }
            },
            Edges = new object[]
            {
                new { Id = "edge-1", Source = "node-1", Target = "node-2" },
                new { Id = "edge-2", Source = "node-2", Target = "node-3", SourceHandle = "true" },
                new { Id = "edge-3", Source = "node-2", Target = "node-4", SourceHandle = "false" },
                new { Id = "edge-4", Source = "node-3", Target = "node-5" },
                new { Id = "edge-5", Source = "node-4", Target = "node-5" }
            }
        };
    }

    #endregion
}
