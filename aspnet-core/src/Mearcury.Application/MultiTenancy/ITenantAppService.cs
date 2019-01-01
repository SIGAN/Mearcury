using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Mearcury.MultiTenancy.Dto;

namespace Mearcury.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

