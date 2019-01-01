using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Mearcury.Configuration;

namespace Mearcury.Web.Host.Startup
{
    [DependsOn(
       typeof(MearcuryWebCoreModule))]
    public class MearcuryWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public MearcuryWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MearcuryWebHostModule).GetAssembly());
        }
    }
}
