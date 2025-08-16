namespace PromptStudio.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when a node is not found
    /// </summary>
    public class NodeNotFoundException : Exception
    {
        public NodeNotFoundException(string message) : base(message)
        {
        }

        public NodeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
