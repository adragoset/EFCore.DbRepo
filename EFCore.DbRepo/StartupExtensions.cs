using System;
using System.Linq;
using System.Reflection;
using EFCoreDbRepo;
using EFCoreDbRepo.Mapping;
using EFCoreDbRepo.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

public static partial class StartupExtensions
{
    public static void AddRepositoryMappings()
    {
        MappingRegistrator.RegisterMappings();
    }
    public static void AddRepositoryFramework<T>(this IServiceCollection services)
    {
        RegisterRepository<T>(services);
        RegisterUnitOfWOrk<T>(services);
    }

    private static void RegisterUnitOfWOrk<T>(IServiceCollection services)
    {
        var all = Assembly
                   .GetEntryAssembly()
                   .GetReferencedAssemblies()
                   .Select(Assembly.Load)
                   .SelectMany(x => x.DefinedTypes)
                   .Where(type => typeof(IUnitOfWork<T>).GetTypeInfo().IsAssignableFrom(type.AsType()));

        foreach (var ti in all)
        {
            var t = ti.AsType();
            if (t.Equals(typeof(IUnitOfWork<T>)))
            {
                services.AddScoped(t, t.DeclaringType);
            }
        }
    }

    private static void RegisterRepository<T>(IServiceCollection services)
    {
        var all = Assembly
                   .GetEntryAssembly()
                   .GetReferencedAssemblies()
                   .Select(Assembly.Load)
                   .SelectMany(x => x.DefinedTypes)
                   .Where(type => typeof(IRepository<T>).GetTypeInfo().IsAssignableFrom(type.AsType()));

        foreach (var ti in all)
        {
            var t = ti.AsType();
            if (t.Equals(typeof(IRepository<T>)))
            {
                services.AddScoped(t, t.DeclaringType);
            }
        }
    }
}