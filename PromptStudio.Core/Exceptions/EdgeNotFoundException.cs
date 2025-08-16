namespace PromptStudio.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when an edge is not found
    /// </summary>
    public class EdgeNotFoundException : Exception
    {
        public EdgeNotFoundException(string message) : base(message)
        {
        }

        public EdgeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
