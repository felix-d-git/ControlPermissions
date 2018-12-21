namespace ControlPermissions
{
    public interface IPermissionControlApplication
    {
        UserType GetUserType(string username);
    }
}
