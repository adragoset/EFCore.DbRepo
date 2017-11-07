using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreDbRepo.Repository {
    public interface IRepository<TDomain> : IDisposable {
        IQueryable<TDomain> DomainSet { get; }

        IQueryable<TDomain> EagerLoadedDomainSet { get; }

        Task<TDomain> GetById(object id);

        Task<TDomain> GetByIdEager(object id);

        Task Insert(TDomain domain_object);

        void Update(TDomain domain_object);

        void Delete(object id);

    }
}