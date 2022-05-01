using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Mearcury;

[Dependency(ReplaceServices = true)]
public class MearcuryBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Mearcury";
}
