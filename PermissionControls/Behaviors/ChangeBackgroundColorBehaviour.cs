using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace ControlPermissions.Behaviors
{
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public class ChangeBackgroundColorBehaviour : IPermissionControlBehavior
    {
        public void AlterControl_NoPermission(UIElement element)
        {
            throw new NotImplementedException();
        }

        public void AlterControl_HasPermission(UIElement element)
        {
            throw new NotImplementedException();
        }
    }
}
