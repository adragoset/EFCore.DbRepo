using AutoMapper;
using EFCoreDbRepo;
using EFCoreDbRepo.Repository;

namespace EFCore.DbRepo.Tests.ConcreteImplementations
{
    public class TestDeepUpdater : IEntityDeepUpdater<Test, TestRecord>
    {
        public void Update(Test source, TestRecord dest, IMapper mapper)
        {
        }
    }
}