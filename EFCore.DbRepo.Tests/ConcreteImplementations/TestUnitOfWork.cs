using EFCoreDbRepo;
using EFCoreDbRepo.Repository;
using EFCoreDbRepo.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EFCore.DbRepo.Tests.ConcreteImplementations
{
    public class TestUnitOfWork : UnitOfWork<Test>
    {
        public TestUnitOfWork(TestRecordContext context, IRepository<Test> repository) : base(context, repository)
        {
        }
    }
}