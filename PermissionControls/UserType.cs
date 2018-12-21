using System;

namespace ControlPermissions
{
    [Flags]
    public enum UserType
    {
        User = 1,
        Operator = 2,
        SuperUser = 4,
        Administrator = 8
    }
}
