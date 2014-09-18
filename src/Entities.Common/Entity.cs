using System.ComponentModel.DataAnnotations;

namespace Entities.Common
{
    public class Entity : IEntity, ISupportsOptimisticConcurrency
    {
        public int Id { get; set; }
        
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}