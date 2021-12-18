using System.Threading.Tasks;
using Abp.Application.Services;
using Mearcury.Sessions.Dto;

namespace Mearcury.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
