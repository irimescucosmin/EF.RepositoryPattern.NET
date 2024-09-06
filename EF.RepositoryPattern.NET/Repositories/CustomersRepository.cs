using EF.RepositoryPattern.NET.Contexts;
using EF.RepositoryPattern.NET.Interfaces;

namespace EF.RepositoryPattern.NET.Repositories;

public class CustomersRepository<TEntity>(CustomersDbContext context)
    : BaseRepository<TEntity, CustomersDbContext>(context), ICustomersRepository<TEntity>
    where TEntity : class, IBaseEntity;