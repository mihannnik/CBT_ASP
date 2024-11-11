using CBT.Domain.Models.Auth;
using CBT.Domain.Models.Enums;

namespace CBT.Domain.Models;

public static class UserPermissions
{
    public static readonly Dictionary<Role, List<Permissions>> RolesPermissions = new()
    {
        [Role.User] = [Permissions.Read, Permissions.Join],
        [Role.Admin] = [Permissions.Read, Permissions.Join, Permissions.Create, Permissions.Modify, Permissions.Delete],
    };
}
