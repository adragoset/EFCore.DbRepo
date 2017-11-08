using EFCore.DbRepo.Tests.ConcreteImplementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.DbRepo.Tests
{
    public class with_a_service_collection_and_test_db_context : with_a_data_base_connection
    {

        public ServiceCollection _services { get; }

        public with_a_service_collection_and_test_db_context():base() {
            _services = new ServiceCollection();
            _services.AddEntityFrameworkInMemoryDatabase().AddDbContext<TestRecordContext>(options => { 
                options.UseInMemoryDatabase("TestDb");
                options.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        }
    }
}