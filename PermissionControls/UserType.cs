using System;

namespace ControlPermissions
{
    [Flags]
    public enum UserType
    {
        User = 1,
        SuperUser = 2,
        Operator = 4,
        Administrator = 8,
        All = 15
    }
}
