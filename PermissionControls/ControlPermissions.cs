using System;
using System.Diagnostics;
using System.Windows;

namespace ControlPermissions
{
    public static class ControlPermissions
    {
        #region NoPermissionBehaviour

        public static readonly DependencyProperty NoPermissionBehaviourProperty = DependencyProperty.RegisterAttached(
            "NoPermissionBehaviour",
            typeof(NoPermissionBehaviour),
            typeof(ControlPermissions),
            new FrameworkPropertyMetadata(
                NoPermissionBehaviour.Disabled, 
                FrameworkPropertyMetadataOptions.AffectsRender, NoPermissionBehaviourPropertyChanged));

        private static void NoPermissionBehaviourPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = (UIElement)d;

            UserType authorizedTypes = GetAuthorizedTypes(element);
            NoPermissionBehaviour noPermissionBehaviour = (NoPermissionBehaviour)e.NewValue;

            ApplyPermissions(element, authorizedTypes, noPermissionBehaviour);
        }

        public static void SetNoPermissionBehaviour(UIElement element, NoPermissionBehaviour behaviour)
        {
            element.SetValue(NoPermissionBehaviourProperty, behaviour);
        }

        public static NoPermissionBehaviour GetNoPermissionBehaviour(UIElement element)
        {
            return (NoPermissionBehaviour)element.GetValue(NoPermissionBehaviourProperty);
        }

        #endregion NoPermissionBehaviour

        #region Authorization

        public static readonly DependencyProperty AuthorizedTypesProperty = DependencyProperty.RegisterAttached(
            "AuthorizedTypes",
            typeof(UserType),
            typeof(ControlPermissions),
            new FrameworkPropertyMetadata(UserType.Administrator, FrameworkPropertyMetadataOptions.AffectsRender, AuthorizedTypesPropertyChanged));

        public static void SetAuthorizedTypes(UIElement element, UserType userType)
        {
            element.SetValue(AuthorizedTypesProperty, userType);
        }

        public static UserType GetAuthorizedTypes(UIElement element)
        {
            return (UserType) element.GetValue(AuthorizedTypesProperty);
        }

        private static void AuthorizedTypesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = (UIElement) d;

            UserType authorizedTypes = (UserType)e.NewValue;
            NoPermissionBehaviour noPermissionBehaviour = GetNoPermissionBehaviour(element);

            ApplyPermissions(element, authorizedTypes, noPermissionBehaviour);
        }

        #endregion

        private static void ApplyPermissions(
            UIElement element, 
            UserType authorizedTypes, 
            NoPermissionBehaviour behaviour)
        {
            if (!(Application.Current is IPermissionControlApplication permissionApp))
            {
                throw new Exception("Application in App.xaml.cs has to implement IPermissionControlApplication in order to apply permissions");
            }

            var currentUserType = permissionApp.GetUserType(Environment.UserName);

            switch (behaviour)
            {
                case NoPermissionBehaviour.Disabled:
                    if (authorizedTypes.HasFlag(currentUserType))
                    {
                        //We are authorized so we set Enabled to true
                        element.SetValue(UIElement.IsEnabledProperty, true);
                        element.SetValue(UIElement.VisibilityProperty, Visibility.Visible);
                        return;
                    }
                    element.SetValue(UIElement.IsEnabledProperty, false);
                    element.SetValue(UIElement.VisibilityProperty, Visibility.Visible);
                    break;
                case NoPermissionBehaviour.Collapsed:
                    if (authorizedTypes.HasFlag(currentUserType))
                    {
                        element.SetValue(UIElement.VisibilityProperty, Visibility.Visible);
                        return;
                    }
                    element.SetValue(UIElement.VisibilityProperty, Visibility.Collapsed);
                    break;
                case NoPermissionBehaviour.Hidden:
                    if (authorizedTypes.HasFlag(currentUserType))
                    {
                        element.SetValue(UIElement.VisibilityProperty, Visibility.Visible);
                        return;
                    }
                    element.SetValue(UIElement.VisibilityProperty, Visibility.Hidden);
                    break;
            }

            Trace.WriteLine($"[{element.GetType().Name}] {currentUserType} has permissions: [{authorizedTypes.HasFlag(currentUserType)}] => Visibility: {element.Visibility}, Enabled: {element.IsEnabled}");
        }
    }
}