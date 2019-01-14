using System;
using System.Windows;

namespace ControlPermissions.Behaviors
{
    public class CustomBehavior : IPermissionControlBehavior
    {
        private readonly Action<UIElement> _noPermissionsAction;
        private readonly Action<UIElement> _hasPermissionsAction;

        public CustomBehavior(
            Action<UIElement> nopermissions,
            Action<UIElement> haspermission)
        {
            _noPermissionsAction = nopermissions;
            _hasPermissionsAction = haspermission;
        }

        public void AlterControl_NoPermission(UIElement element)
        {
            _noPermissionsAction.Invoke(element);
        }

        public void AlterControl_HasPermission(UIElement element)
        {
            _hasPermissionsAction.Invoke(element);
        }
    }
}
