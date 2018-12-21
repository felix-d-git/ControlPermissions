using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ControlPermissions;

namespace Sample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IPermissionControlApplication
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var window = new MainWindow();
            window.Title = $"MainWindow - [{GetUserType(Environment.UserName)}]";
            MainWindow = window;
            MainWindow.Show();
        }

        public UserType GetUserType(string username)
        {
            return UserType.Administrator;
        }
    }
}
