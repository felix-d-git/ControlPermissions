using System;
using System.Collections.Generic;
using System.Windows;
using ControlPermissions;
using ControlPermissions.Behaviors;
using Sample.ViewModels;
using Sample.Windows;

namespace Sample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            MainWindow = new MainWindow();

            ControlPermissionHelper.Initialize(
                TriggerControlPermissionUpdate, 
                Validate,
                GetBehaviors()
            );

            MainWindow?.Show();
        }

        private List<IPermissionControlBehavior> GetBehaviors()
        {
            return new List<IPermissionControlBehavior>
                   {
                       new SetTooltipBehavior(true, lang._no_permission)
                   };
        }
    
        private bool Validate(UIElement element)
        {
            //Here you can specify the validation made by the controls
            AccessLevel controlMinimumAccessLevel = (AccessLevel)element.GetValue(ControlPermissions.ControlPermissions.AccessLevelProperty);

            if (controlMinimumAccessLevel == AccessLevel.Everybody)
            {
                return true;
            }

            //Valide currentUser via DB or sth..
            if (MainWindow?.DataContext == null)
            {
                return false;
            }
            AccessLevel userAccessLevel = (MainWindow.DataContext as MainWindowViewModel).GetDemoAccessType(Environment.UserName);

            if (userAccessLevel >= controlMinimumAccessLevel)
            {
                return true;
            }
            return false;
        }


        private void TriggerControlPermissionUpdate()
        {
            ((MainWindowViewModel)MainWindow.DataContext).PropertyChanged += (s, o) =>
            {
                if (o.PropertyName == "AccessLevel")
                    ControlPermissionHelper.TriggerValidationPropertyChanged();
            };
        }
    }
}
