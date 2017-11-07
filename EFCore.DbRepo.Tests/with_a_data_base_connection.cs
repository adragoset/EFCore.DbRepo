using System;
using EFCore.DbRepo.Tests.ConcreteImplementations;
using Microsoft.EntityFrameworkCore;

namespace EFCore.DbRepo.Tests
{
    public abstract class with_a_data_base_connection : IDisposable
    {

        public TestRecordContext _testRecordContext { get; private set; }

        public with_a_data_base_connection() {
            InitDb();
        }

        public void InitDb() {
            var builder = new DbContextOptionsBuilder<TestRecordContext>().UseInMemoryDatabase("AuthService");
            var context = new TestRecordContext(builder.Options);
            _testRecordContext = context;
        }

        public async void Dispose() {
            var users = await _testRecordContext.UserPrincipleRecords.ToArrayAsync();
            _testRecordContext.RemoveRange(users);
            _testRecordContext.SaveChanges();
        }
    }
}