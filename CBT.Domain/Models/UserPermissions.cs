using CBT.Domain.Models.Auth;
using CBT.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Domain.Models
{
    public static class UserPermissions
    {
        public static readonly Dictionary<Role, List<Permissions>> RolesPermissions = new()
        {
            {Role.User, new List<Permissions> { Permissions.Read, Permissions.Join } },
            {Role.Admin, new List<Permissions> { Permissions.Read, Permissions.Join, Permissions.Create, Permissions.Modify, Permissions.Delete }},
        };
    }
}
