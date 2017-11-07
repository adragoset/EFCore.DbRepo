using AutoMapper;

namespace EFCoreDbRepo.Repository {
    public interface IEntityDeepUpdater<TDomain, TEntity> {
        void Update(TDomain source, TEntity dest, IMapper mapper);
    }
}