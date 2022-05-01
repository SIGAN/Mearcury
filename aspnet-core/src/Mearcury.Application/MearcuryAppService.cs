using System;
using System.Collections.Generic;
using System.Text;
using Mearcury.Localization;
using Volo.Abp.Application.Services;

namespace Mearcury;

/* Inherit your application services from this class.
 */
public abstract class MearcuryAppService : ApplicationService
{
    protected MearcuryAppService()
    {
        LocalizationResource = typeof(MearcuryResource);
    }
}
