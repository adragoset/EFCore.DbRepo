using System;
using AutoMapper;
using AutoMapper.Configuration;
using EFCore.DbRepo.Tests.ConcreteImplementations;
using Microsoft.EntityFrameworkCore;

namespace EFCore.DbRepo.Tests
{
    public abstract class with_a_test_record_repo : with_a_data_base_connection
    {
        public IMapper _aMapper;

        public TestRepo _testRepo { get; private set; }
        public TestUnitOfWork _unitOfWork { get; private set; }

        public with_a_test_record_repo():base() {
            var baseMappings = new MapperConfigurationExpression();
            var _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                
                cfg.AddProfile(new TestRecordMapping());
            });

            _aMapper = _mapperConfiguration.CreateMapper();

            _testRepo = new TestRepo(_testRecordContext, _aMapper, new TestDeepUpdater());

            _unitOfWork = new TestUnitOfWork(_testRecordContext, _testRepo);
            
        }

        public override void Dispose() {
            base.Dispose();
        }
    }
}