using Ardalis.Specification;
using Tutorial.ProjectManagement.SharedKernel.Contracts.Database;

namespace Tutorial.ProjectManagement.SharedKernel.Contracts.DataAccess;

// from Ardalis.Specification
public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
