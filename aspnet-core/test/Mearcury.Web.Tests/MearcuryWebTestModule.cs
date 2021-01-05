using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Mearcury.EntityFrameworkCore;
using Mearcury.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Mearcury.Web.Tests
{
    [DependsOn(
        typeof(MearcuryWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class MearcuryWebTestModule : AbpModule
    {
        public MearcuryWebTestModule(MearcuryEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MearcuryWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(MearcuryWebMvcModule).Assembly);
        }
    }
}