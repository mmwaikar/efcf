namespace Entities.Common
{
    /// <summary>
    /// Specifies if the table backing the entity supports optimistic 
    /// concurrency.
    /// </summary>
    public interface ISupportsOptimisticConcurrency
    {
        /// <summary>
        /// Used for concurrency checking.
        /// </summary>
        byte[] RowVersion { get; set; } 
    }
}