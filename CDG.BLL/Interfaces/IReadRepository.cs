using Ardalis.Specification;

namespace CDG.BLL.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
