using Ardalis.Specification.EntityFrameworkCore;
using CDG.BLL.Interfaces;

namespace CDG.DAL.Data;

public class Repository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public Repository(AppDbContext context) : base(context)
    {
    }
}