using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Enum
{
    public enum UserRole
    {
        Admin = 1,
        Manager = 2,
        User = 3,
    }
    public static class Role
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";

    }
}
