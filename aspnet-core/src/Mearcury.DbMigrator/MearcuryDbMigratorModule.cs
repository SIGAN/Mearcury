using Mearcury.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Mearcury.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MearcuryEntityFrameworkCoreModule),
    typeof(MearcuryApplicationContractsModule)
    )]
public class MearcuryDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
