using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ControlPermissions;

namespace Sample.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            _accessLevels = Enum.GetValues(typeof(AccessLevel)).Cast<AccessLevel>().Select(ut => ut.ToString()).ToList();
            //User cannot have all roles !
            _accessLevels.Remove("Everybody");
        }

        private List<string> _accessLevels;
        public List<string> AccessLevels
        {
            get { return _accessLevels; }
            set
            {
                _accessLevels = value;
                OnPropertyChanged();
            }
        }

        private AccessLevel _accessLevel = AccessLevel.Users;

        public AccessLevel AccessLevel
        {
            get => _accessLevel;
            set
            {
                if (_accessLevel == value)
                {
                    return;
                }
                _accessLevel = value;
                OnPropertyChanged();
            }
        }

        public delegate void AccessTypeChangedHandler();


        public AccessLevel GetDemoAccessType(string userName)
        {
            return AccessLevel;
        }



        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
