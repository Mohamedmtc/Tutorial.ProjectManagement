using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial.ProjectManagement.Presistance.Context
{
    public partial class DatabaseService
    {
        public int DBSaveChanges()
        {
            return this.SaveChanges();
        }
        public async Task<int> DBSaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
        }
    }
}
