namespace PromptStudio.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when a flow service operation fails
    /// </summary>
    public class FlowServiceException : Exception
    {
        public FlowServiceException(string message) : base(message)
        {
        }

        public FlowServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
