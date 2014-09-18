using System;

namespace Entities.Common
{
    public interface IAuditable
    {
        DateTime CreateDate { get; set; }
        DateTime UpdateDate { get; set; }
    }
}