namespace Entities.Common
{
    public interface ILookup : IEntity
    {
        string Name { get; set; }
        string Description { get; set; }
    }
}