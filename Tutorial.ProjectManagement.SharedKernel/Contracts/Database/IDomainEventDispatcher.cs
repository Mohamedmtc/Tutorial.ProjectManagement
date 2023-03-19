using Tutorial.ProjectManagement.SharedKernel.DataBase;

namespace Tutorial.ProjectManagement.SharedKernel.Contracts.Database;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents);
}
