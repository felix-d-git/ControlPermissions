using System.Windows;

namespace ControlPermissions.Behaviors
{
    public interface IPermissionControlBehavior
    {
        void AlterControl_NoPermission(UIElement element);

        void AlterControl_HasPermission(UIElement element);
    }
}
