using System.Linq;
using System.Reflection;
using AutoMapper;

namespace EFCoreDbRepo.Mapping
{
    public class MappingRegistrator {
        public static void RegisterMappings() {
            var all = Assembly
               .GetEntryAssembly()
               .GetReferencedAssemblies()
               .Select(Assembly.Load)
               .SelectMany(x => x.DefinedTypes)
               .Where(type => typeof(IDomainMapping).GetTypeInfo().IsAssignableFrom(type.AsType()));

            foreach (var ti in all)
            {
                var t = ti.AsType();
                if (t.Equals(typeof(IDomainMapping)))
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.AddProfiles(t); // Initialise each Profile classe
                    });
                }
            }
        }
    }
}