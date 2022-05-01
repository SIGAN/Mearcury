using Mearcury.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Mearcury.Permissions;

public class MearcuryPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MearcuryPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(MearcuryPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MearcuryResource>(name);
    }
}
