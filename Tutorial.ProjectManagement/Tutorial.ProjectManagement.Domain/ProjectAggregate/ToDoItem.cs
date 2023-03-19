

using System.Xml.Linq;
using Tutorial.ProjectManagement.SharedKernel.DataBase;

namespace Tutorial.PhoneBook.Domain.ProjectAggregate;

public class ToDoItem : EntityBase
{
    public string Title { get; private set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? ContributorId { get; private set; }
    public bool IsDone { get; private set; }

    public ToDoItem(string title)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentNullException(nameof(title));
        Title = title;
    }

    public void MarkComplete()
    {
        if (!IsDone)
        {
            IsDone = true;
        }
    }

    public void AddContributor(int contributorId)
    {
        if (contributorId <= 0)
            throw new ArgumentNullException(nameof(contributorId));
        ContributorId = contributorId;
    }

    public void RemoveContributor()
    {
        ContributorId = null;
    }

    public override string ToString()
    {
        string status = IsDone ? "Done!" : "Not done.";
        return $"{Id}: Status: {status} - {Title} - {Description}";
    }
}
