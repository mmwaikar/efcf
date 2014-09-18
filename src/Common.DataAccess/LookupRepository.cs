using System.Linq;
using Entities.Common;

namespace Common.DataAccess
{
    public class LookupRepository<TLookup> : Repository<TLookup>, ILookupRepository<TLookup>
        where TLookup : Lookup
    {
        public LookupRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public TLookup FindByName(string name)
        {
            return _dbSet.First(e => e.Name == name);
        }
    }
}