using System.Threading.Tasks;
using Abp.Application.Services;
using Mearcury.Authorization.Accounts.Dto;

namespace Mearcury.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
