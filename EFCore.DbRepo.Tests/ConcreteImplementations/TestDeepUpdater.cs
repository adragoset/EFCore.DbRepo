using AutoMapper;
using EFCoreDbRepo.Repository;

namespace EFCore.DbRepo.Tests.ConcreteImplementations
{
    public class TestDeepUpdater : IEntityDeepUpdater<Test, TestRecord>
    {
        public void Update(Test source, TestRecord dest, IMapper mapper)
        {
            var entity = mapper.Map<Test, TestRecord>(source);
            mapper.Map(entity, dest, entity.GetType(), dest.GetType());
        }
    }
}