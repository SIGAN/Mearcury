using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Mearcury.Controllers
{
    public abstract class MearcuryControllerBase: AbpController
    {
        protected MearcuryControllerBase()
        {
            LocalizationSourceName = MearcuryConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
