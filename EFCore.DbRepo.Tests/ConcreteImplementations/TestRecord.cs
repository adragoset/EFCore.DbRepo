using System;
using EFCoreDbRepo.EntityBase;

namespace EFCore.DbRepo.Tests.ConcreteImplementations
{
    public class TestRecord: IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}