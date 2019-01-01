using Abp.Application.Services.Dto;

namespace Mearcury.MultiTenancy.Dto
{
    public class PagedTenantResultRequestDto : PagedResultRequestDto
    {
        public string TenancyName { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}

