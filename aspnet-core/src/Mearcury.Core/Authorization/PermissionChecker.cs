using Abp.Authorization;
using Mearcury.Authorization.Roles;
using Mearcury.Authorization.Users;

namespace Mearcury.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
