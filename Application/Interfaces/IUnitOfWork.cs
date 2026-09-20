using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        public Task BeginTransactionAsync ();
        public Task CommitAsync();
        public Task RollbackAsync();
        public Task SaveChangesAsync();
    }
}
