namespace EF.RepositoryPattern.NET.Interfaces;

public interface ICustomersRepository<T> : IBaseRepository<T> where T : IBaseEntity;