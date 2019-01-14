using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using ControlPermissions.Behaviors;

namespace ControlPermissions
{
    public class ControlPermissionHelper
    {
        private static bool _isInitialized = false;


        private static List<IPermissionControlBehavior> _behaviors = new List<IPermissionControlBehavior>()
                                                                       {
                                                                           new DefaultBehavior()
                                                                       };

        private static readonly List<UIElement> _uiElements = new List<UIElement>();

        private static Func<UIElement, bool> Validator;
        
        public ControlPermissionHelper(
            List<IPermissionControlBehavior> behaviors = null)
        {
            if(behaviors != null)
                _behaviors = behaviors;
            

            ApplyOnce();
        }

        private void ApplyOnce()
        {
            if (Application.Current?.MainWindow == null)
            {
                return;
            }

            if (Application.Current.MainWindow.DataContext == null)
            {
                return;
            }
            ApplyBehaviors();
        }

        public void SetValidator(Func<UIElement, bool> validationFunction)
        {
            Validator = validationFunction;
        }



        public void AddBehavior(IPermissionControlBehavior behavior)
        {
            _behaviors.Add(behavior);
        }

        public void RemoveBehavior(IPermissionControlBehavior behavior)
        {
            _behaviors.Remove(behavior);
        }

        public void ClearBehaviors()
        {
            _behaviors.Clear();
        }

        public static void ApplyBehaviors(UIElement element)
        {
            if (!_uiElements.Contains(element))
            {
                _uiElements.Add(element);
            }

            if (!_isInitialized)
            {
                return;
            }

            try
            {
                bool hasPermissions = Validate(element);

                foreach (IPermissionControlBehavior behavior in _behaviors)
                {
                    if (hasPermissions)
                    {
                        behavior.AlterControl_HasPermission(element);
                    }
                    else
                    {
                        behavior.AlterControl_NoPermission(element);
                    }
                }
            }
            catch (Exception ex)
            {
                //Debug.Fail(ex.ToString());
                Trace.WriteLine($"Error: Error {ex}");
            }
        }

        private static bool Validate(UIElement element)
        {
            try
            {
                return Validator.Invoke(element);
            }
            catch (Exception ex)
            {
                throw new Exception("Validation impossible", ex);
            }
        }

        public static void ApplyBehaviors()
        {
            foreach (UIElement element in _uiElements)
            {
                ApplyBehaviors(element);
            }
        }

        public static void TriggerValidationPropertyChanged()
        {
            ApplyBehaviors();
        }

        public static void Initialize(
            Action trigger,
            Func<UIElement, bool> validator,  
            List<IPermissionControlBehavior> permissionControlBehaviors = null)
        {
            Validator = validator;
            _behaviors = new List<IPermissionControlBehavior>();
            _behaviors.Add(new DefaultBehavior());

            if (permissionControlBehaviors != null)
            {
                _behaviors.AddRange(permissionControlBehaviors);
            }

            trigger.Invoke();

            _isInitialized = true;
            TriggerValidationPropertyChanged();
        }

        
    }
}
