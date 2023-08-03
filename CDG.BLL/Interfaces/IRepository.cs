using Ardalis.Specification;

namespace CDG.BLL.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
