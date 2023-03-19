using Ardalis.Specification;
using Tutorial.ProjectManagement.SharedKernel.Contracts.Database;

namespace Tutorial.ProjectManagement.SharedKernel.Contracts.DataAccess;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
