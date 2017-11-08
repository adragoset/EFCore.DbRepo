using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using EFCoreDbRepo;
using EFCoreDbRepo.Mapping;
using EFCoreDbRepo.Repository;
using EFCoreDbRepo.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

public static partial class StartupExtensions
{
    public static IEnumerable<Type> AddRepositoryMappings(Assembly assembly)
    {
        var assemblies = assembly.GetReferencedAssemblies();

        foreach (var ti in assembly.DefinedTypes)
        {
            if (ti.ImplementedInterfaces.Contains(typeof(IDomainMapping)))
            {
                yield return ti.AsType();
            }
        }

        foreach (var assemblyName in assemblies)
        {
            assembly = Assembly.Load(assemblyName);

            foreach (var ti in assembly.DefinedTypes)
            {
                if (ti.ImplementedInterfaces.Contains(typeof(IDomainMapping)))
                {
                    yield return ti.AsType();
                }
            }
        }
    }
    public static void AddRepositoryFramework<T>(this IServiceCollection services, Assembly assembly)
    {
        RegisterEntityDeepUpdaters(assembly, services);
        RegisterRepository<T>(assembly, services);
        RegisterUnitOfWork<T>(assembly, services);
    }

    private static void RegisterUnitOfWork<T>(Assembly assembly, IServiceCollection services)
    {
        var assemblies = assembly.GetReferencedAssemblies();

        foreach (var ti in assembly.DefinedTypes)
        {
            if (ti.ImplementedInterfaces.Contains(typeof(IUnitOfWork<T>)))
            {
                services.AddScoped(ti.BaseType.GetInterfaces()[0], ti.AsType());
            }
        }

        foreach (var assemblyName in assemblies)
        {
            assembly = Assembly.Load(assemblyName);

            foreach (var ti in assembly.DefinedTypes)
            {
                if (ti.ImplementedInterfaces.Contains(typeof(IUnitOfWork<T>)))
                {
                    services.AddScoped(ti.BaseType.GetInterfaces()[0], ti.AsType());
                }
            }
        }
    }

    private static void RegisterRepository<T>(Assembly assembly, IServiceCollection services)
    {
        var assemblies = assembly.GetReferencedAssemblies();

        foreach (var ti in assembly.DefinedTypes)
        {
            if (ti.ImplementedInterfaces.Contains(typeof(IRepository<T>)))
            {
                services.AddScoped(ti.BaseType.GetInterfaces()[0], ti.AsType());
            }
        }

        foreach (var assemblyName in assemblies)
        {
            assembly = Assembly.Load(assemblyName);

            foreach (var ti in assembly.DefinedTypes)
            {
                if (ti.ImplementedInterfaces.Contains(typeof(IRepository<T>)))
                {
                    services.AddScoped(ti.BaseType.GetInterfaces()[0], ti.AsType());
                }
            }
        }
    }

    private static void RegisterEntityDeepUpdaters(Assembly assembly, IServiceCollection services) {
         var assemblies = assembly.GetReferencedAssemblies();

        foreach (var ti in assembly.DefinedTypes)
        {
            if (ti.ImplementedInterfaces.Contains(typeof(IEntityDeepUpdater)))
            {
                services.AddScoped(ti.GetInterfaces()[0], ti.AsType());
            }
        }

        foreach (var assemblyName in assemblies)
        {
            assembly = Assembly.Load(assemblyName);

            foreach (var ti in assembly.DefinedTypes)
            {
                var type = typeof(IEntityDeepUpdater);
                if (ti.ImplementedInterfaces.Contains(type) && ti.ImplementedInterfaces.Count()  != 1)
                {
                    services.AddScoped(ti.GetInterfaces()[0], ti.AsType());
                }
            }
        }
    }
}