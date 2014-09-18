using System.ComponentModel.DataAnnotations;

namespace Entities.Common
{
    public class Lookup : Entity, ILookup
    {
        public Lookup()
        {
            Description = string.Empty;
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}