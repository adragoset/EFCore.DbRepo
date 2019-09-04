using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreDbRepo.Repository;

namespace EFCore.DbRepo.Tests.ConcreteImplementations
{
    public class TestRepo : Repository<Test, TestRecord>
    {
        public TestRepo(TestRecordContext db, IMapper mapper, IEntityDeepUpdater<Test, TestRecord> updater) : base(db, mapper, updater)
        {
        }

        public override IQueryable<Test> EagerLoadedDomainSet {
            get {
                return DbSet.ProjectTo<Test>(Mapper.ConfigurationProvider);
            }
        }

        public override async Task<Test> GetByIdEager(object id)
        {
            var userPrinciple = DbSet.Where( e => e.Id == (Guid)id).First();
            return await Task.Run( () => MapEntityToDomain(userPrinciple));
        }

        public override Task Insert(Test domain_object)
        {
            return this.DefaultInsert(domain_object);
        }

        public override void Update(Test domain_object)
        {
            this.DefaultUpdate(domain_object);
        }
    }
}