using System.Linq;
using System.Threading.Tasks;
using EFCoreDbRepo.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCoreDbRepo.UnitOfWork {
    public abstract class UnitOfWork<TDomain> : IUnitOfWork<TDomain> {
        private readonly DbContext _context;
        public IRepository<TDomain> Repository { get; private set; }
        private bool disposed = false;

        public UnitOfWork(DbContext context, IRepository<TDomain> repository) {
            _context = context;
            this.Repository = repository;
        }

        public IDbContextTransaction BeginTransaction() {
            return _context.Database.BeginTransaction();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync() {
            return _context.Database.BeginTransactionAsync();
        }

        public Task<int> SaveAsync() {
            return _context.SaveChangesAsync();
        }

        public void ClearContext() {
            foreach (var entity in _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)) {
                _context.Entry(entity.Entity).State = EntityState.Detached;
            }
        }

        public void Dispose() {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        public void Save() {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    Repository.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}