using EFCoreDbRepo;
using EFCoreDbRepo.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EFCore.DbRepo.Tests.ConcreteImplementations
{
    public class TestUnitOfWork : UnitOfWork<Test>
    {
        public TestUnitOfWork(DbContext context, IRepository<Test> repository) : base(context, repository)
        {
        }
    }
}