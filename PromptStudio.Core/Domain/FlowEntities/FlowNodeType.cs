namespace PromptStudio.Core.Domain;

/// <summary>
/// Types of nodes in a workflow
/// </summary>
public enum FlowNodeType
{
    Input = 0,
    Prompt = 1,
    Variable = 2,
    Conditional = 3,
    Transform = 4,
    Output = 5,
    LlmCall = 6,
    Template = 7,
    Loop = 8,
    Parallel = 9,
    ApiCall = 10,
    Validation = 11,
    Aggregation = 12,
    ErrorHandler = 13,
    UserInput = 14
}