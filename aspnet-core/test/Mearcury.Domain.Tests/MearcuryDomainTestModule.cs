using Mearcury.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Mearcury;

[DependsOn(
    typeof(MearcuryEntityFrameworkCoreTestModule)
    )]
public class MearcuryDomainTestModule : AbpModule
{

}
