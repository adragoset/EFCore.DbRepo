using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using EFCore.DbRepo.Tests.ConcreteImplementations;
using EFCoreDbRepo.Repository;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EFCore.DbRepo.Tests
{
    public class when_using_startup_extensions: with_a_service_collection_and_test_db_context
    {
        private Assembly _assembly;
        private MapperConfiguration mapperConfig;

        private IMapper _mapper;
        private ServiceProvider _container;

        public when_using_startup_extensions() {
            var test = new TestRecordMapping();
            _assembly =  test.GetType().Assembly; //make sure the assembly is loaded.
            mapperConfig = new MapperConfiguration(cfg => cfg.AddProfiles(StartupExtensions.AddRepositoryMappings(test.GetType().Assembly)));
            _mapper = mapperConfig.CreateMapper();
            _services.AddSingleton<IMapper>(_mapper);
            _services.AddRepositoryFramework<Test>(_assembly);
            _container = _services.BuildServiceProvider();
        }

        [Fact]
        public void it_adds_the_test_mapping() {
            Assert.Contains("EFCore.DbRepo.Tests.ConcreteImplementations.TestRecordMapping", mapperConfig.Profiles.Select(p => p.Name));
        }

        [Fact]
        public void it_adds_the_repositoriy() {
            
            var repo = _container.GetService<IRepository<Test>>();
            Assert.NotNull(repo);
        }
    }
}