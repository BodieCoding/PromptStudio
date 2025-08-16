namespace PromptStudio.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when a flow is not found
    /// </summary>
    public class FlowNotFoundException : Exception
    {
        public FlowNotFoundException(string message) : base(message)
        {
        }

        public FlowNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
