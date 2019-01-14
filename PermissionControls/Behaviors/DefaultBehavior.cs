using System.Windows;

namespace ControlPermissions.Behaviors
{
    public class DefaultBehavior : IPermissionControlBehavior
    {
        public void AlterControl_NoPermission(UIElement element)
        {
            UIElement uiElement = element;
            uiElement.IsEnabled = false;
        }

        public void AlterControl_HasPermission(UIElement element)
        {
            UIElement uiElement = element;
            uiElement.IsEnabled = true;
        }
    }
}
