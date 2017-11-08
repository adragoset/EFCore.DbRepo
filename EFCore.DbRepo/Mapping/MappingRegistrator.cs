using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace EFCoreDbRepo.Mapping
{
    public class MappingRegistrator
    {
        public static List<Type> RegisterMappings(Assembly assembly) {
            return GetAll(assembly).ToList();
        }

        public static IEnumerable<Type> GetAll(Assembly assembly)
        {
            var assemblies = assembly.GetReferencedAssemblies();

            foreach (var ti in assembly.DefinedTypes)
            {
                if (ti.ImplementedInterfaces.Contains(typeof(IDomainMapping)))
                {
                    yield return ti.AsType();
                }
            }

            foreach (var assemblyName in assemblies) {
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
    }
}