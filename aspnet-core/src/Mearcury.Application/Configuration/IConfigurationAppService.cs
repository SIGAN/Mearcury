using System.Threading.Tasks;
using Mearcury.Configuration.Dto;

namespace Mearcury.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
