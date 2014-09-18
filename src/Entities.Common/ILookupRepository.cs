namespace Entities.Common
{
    public interface ILookupRepository<TLookup> : IRepository<TLookup>
    {
        TLookup FindByName(string name);
    }
}