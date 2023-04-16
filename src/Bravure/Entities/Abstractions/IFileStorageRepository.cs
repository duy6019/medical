namespace Bravure.Entities.Abstractions
{
    public interface IBaseRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
