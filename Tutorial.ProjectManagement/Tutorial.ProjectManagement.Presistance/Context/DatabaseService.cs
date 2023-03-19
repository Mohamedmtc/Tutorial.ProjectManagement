using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Tutorial.ProjectManagement.Application.Contracts;

namespace Tutorial.ProjectManagement.Presistance.Context
{
    public partial class DatabaseService : DbContext, IDataBaseService
    {


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
                optionsBuilder.UseSqlServer("Server=.;Initial Catalog=SQlDBPhoneBook;Integrated Security=true;TrustServerCertificate=True;");
        }
    }
}
