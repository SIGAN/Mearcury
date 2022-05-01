using Volo.Abp.Modularity;

namespace Mearcury;

[DependsOn(
    typeof(MearcuryApplicationModule),
    typeof(MearcuryDomainTestModule)
    )]
public class MearcuryApplicationTestModule : AbpModule
{

}
