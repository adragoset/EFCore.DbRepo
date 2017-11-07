using System;
using System.Threading.Tasks;
using EFCoreDbRepo.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCoreDbRepo.UnitOfWork
{
    public interface IUnitOfWork<TDomain> : IDisposable {
        IRepository<TDomain> Repository { get; }

        IDbContextTransaction BeginTransaction();

        Task<IDbContextTransaction> BeginTransactionAsync();

        void ClearContext();

        void Save();

        Task<int> SaveAsync();
    }
}