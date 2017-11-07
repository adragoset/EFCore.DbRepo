using System;

namespace EFCoreDbRepo.EntityBase
{
    public interface IAggregateRoot
    {
        Guid Id { get; set; }
    }
}