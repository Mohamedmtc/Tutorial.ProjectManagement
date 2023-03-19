using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Tutorial.PhoneBook.Domain.ContributorAggregate;
using Tutorial.PhoneBook.Domain.ProjectAggregate;
using Tutorial.ProjectManagement.Application.Contracts.Database;

namespace Tutorial.ProjectManagement.Presistance.Context
{
    public partial class DatabaseService : DbContext, IDataBaseService
    {
        public DbSet<ToDoItem> ToDoItems { get ; set ; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Contributor> Contributors { get; set; }

        public DatabaseService()
        {
        }

        public DatabaseService(DbContextOptions<DatabaseService> options)
            : base(options)
        {
            Database.EnsureCreated();

        }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=.;Initial Catalog=SQlDBProjects;Integrated Security=true;TrustServerCertificate=True;");
        }
    }
}
