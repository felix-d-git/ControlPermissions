using System.Windows;

namespace ControlPermissions
{
    public class ControlPermissions
    {
        #region Authorization

        public static readonly DependencyProperty AccessLevelProperty = DependencyProperty.RegisterAttached(
            "AccessLevel",
            typeof(AccessLevel),
            typeof(ControlPermissions),
            new FrameworkPropertyMetadata(AccessLevel.Everybody, FrameworkPropertyMetadataOptions.AffectsRender, AuthorizedTypesPropertyChanged));

        public static void SetAccessLevel(UIElement element, AccessLevel accessLevel)
        {
            element.SetValue(AccessLevelProperty, accessLevel);
        }

        public static AccessLevel GetAccessLevel(UIElement element)
        {
            return (AccessLevel) element.GetValue(AccessLevelProperty);
        }

        private static void AuthorizedTypesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = (UIElement) d;
            ApplyPermissions(element);
        }

        #endregion

        private static void ApplyPermissions(
            UIElement element)
        {
            ControlPermissionHelper.ApplyBehaviors(element);
        }
    }
}