using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Repositories.AllSharedIRepository
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
    }
}
