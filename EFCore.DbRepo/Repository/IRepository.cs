using System;
using System.Linq;

namespace EFCoreDbRepo {
    public interface IRepository<TDomain> : IDisposable {
        IQueryable<TDomain> DomainSet { get; }

        IQueryable<TDomain> EagerLoadedDomainSet { get; }

        TDomain GetById(object id);

        TDomain GetByIdEager(object id);

        void Insert(TDomain domain_object);

        void Update(TDomain domain_object);

        void Delete(object id);

    }
}