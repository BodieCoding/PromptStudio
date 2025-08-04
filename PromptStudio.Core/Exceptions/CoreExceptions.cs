namespace PromptStudio.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when a requested resource is not found
    /// </summary>
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message) { }
        public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when attempting to create a resource with a duplicate name
    /// </summary>
    public class DuplicateResourceException : Exception
    {
        public DuplicateResourceException(string message) : base(message) { }
        public DuplicateResourceException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when validation fails
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when attempting to create a template with a duplicate name
    /// </summary>
    public class DuplicateTemplateNameException : Exception
    {
        public DuplicateTemplateNameException(string message) : base(message) { }
        public DuplicateTemplateNameException(string message, Exception innerException) : base(message, innerException) { }
    }
}
