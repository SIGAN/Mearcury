using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Mearcury.Configuration.Dto;

namespace Mearcury.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : MearcuryAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
