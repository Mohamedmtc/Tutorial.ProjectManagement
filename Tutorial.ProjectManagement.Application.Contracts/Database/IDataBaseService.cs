using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Tutorial.PhoneBook.Domain.ContributorAggregate;
using Tutorial.PhoneBook.Domain.ProjectAggregate;

namespace Tutorial.ProjectManagement.Application.Contracts.Database
{
    public interface IDataBaseService
    {
        DbSet<ToDoItem> ToDoItems { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Contributor> Contributors { get; set; }
        int DBSaveChanges();
        Task<int> DBSaveChangesAsync(CancellationToken cancellationToken = default);

    }

}
