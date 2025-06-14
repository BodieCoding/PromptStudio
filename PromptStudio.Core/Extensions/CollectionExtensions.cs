namespace PromptStudio.Core.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Checks if an IEnumerable is null or has no elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The IEnumerable to check.</param>
        /// <returns>True if the source is null or empty; otherwise, false.</returns>
        public static bool HasNoElements<T>(this IEnumerable<T>? source)
        {
            return source == null || !source.Any();
        }

        /// <summary>
        /// Checks if an ICollection is null or has no elements.
        /// This version can be slightly more performant for types that implement ICollection by checking Count.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The ICollection to check.</param>
        /// <returns>True if the source is null or empty; otherwise, false.</returns>
        public static bool HasNoElements<T>(this ICollection<T>? source)
        {
            return source == null || source.Count == 0;
        }

        /// <summary>
        /// Checks if an IEnumerable is not null and contains at least one element.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The IEnumerable to check.</param>
        /// <returns>True if the source is not null and has elements; otherwise, false.</returns>
        public static bool HasElements<T>(this IEnumerable<T>? source)
        {
            return source != null && source.Any();
        }

        /// <summary>
        /// Checks if an ICollection is not null and contains at least one element.
        /// This version can be slightly more performant for types that implement ICollection by checking Count.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The ICollection to check.</param>
        /// <returns>True if the source is not null and has elements; otherwise, false.</returns>
        public static bool HasElements<T>(this ICollection<T>? source)
        {
            return source != null && source.Count > 0;
        }
    }
}
