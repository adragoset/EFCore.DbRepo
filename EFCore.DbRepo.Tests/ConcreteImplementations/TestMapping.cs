using AutoMapper;
using EFCoreDbRepo.Mapping;

namespace EFCore.DbRepo.Tests.ConcreteImplementations
{
    public class TestRecordMapping: Profile, IDomainMapping
    {
        public TestRecordMapping()
        {
            CreateMap<TestRecord, Test>().ForMember(dest => dest.Identifier, opts => opts.MapFrom(src => src.Id)).ForMember(dest => dest.NotName, opts => opts.MapFrom(src => src.Name));
            CreateMap<Test ,TestRecord>().ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Identifier)).ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.NotName));
            CreateMap<TestRecord, TestRecord>();
        }
    }
}