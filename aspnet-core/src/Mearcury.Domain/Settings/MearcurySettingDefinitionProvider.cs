using Volo.Abp.Settings;

namespace Mearcury.Settings;

public class MearcurySettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MearcurySettings.MySetting1));
    }
}
