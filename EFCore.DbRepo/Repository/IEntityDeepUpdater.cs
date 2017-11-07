using AutoMapper;

namespace EFCoreDbRepo {
    public interface IEntityDeepUpdater<TDomain, TEntity> {
        void Update(TDomain source, TEntity dest, IMapper mapper);
    }
}