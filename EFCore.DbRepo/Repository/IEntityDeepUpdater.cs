using AutoMapper;

namespace EFCoreDbRepo.Repository {
    public interface IEntityDeepUpdater
    {
    }
    public interface IEntityDeepUpdater<TDomain, TEntity> : IEntityDeepUpdater {
        void Update(TDomain source, TEntity dest, IMapper mapper);
    }
}