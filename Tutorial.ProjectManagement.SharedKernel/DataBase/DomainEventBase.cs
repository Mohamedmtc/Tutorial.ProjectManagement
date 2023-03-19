using MediatR;

namespace Tutorial.ProjectManagement.SharedKernel.DataBase;

public abstract class DomainEventBase : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
