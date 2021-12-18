using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Mearcury.Authorization;

namespace Mearcury
{
    [DependsOn(
        typeof(MearcuryCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MearcuryApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MearcuryAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MearcuryApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
