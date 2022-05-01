using Mearcury.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Mearcury.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MearcuryController : AbpControllerBase
{
    protected MearcuryController()
    {
        LocalizationResource = typeof(MearcuryResource);
    }
}
