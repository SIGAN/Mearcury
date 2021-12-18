using Abp.Application.Services;
using Mearcury.MultiTenancy.Dto;

namespace Mearcury.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

