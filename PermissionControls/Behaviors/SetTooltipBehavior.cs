using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ControlPermissions.Behaviors
{
    public class SetTooltipBehavior : IPermissionControlBehavior
    {
        private readonly Dictionary<string, string> _backupValues = new Dictionary<string, string>();

        private readonly string _noPermissionTooltip;
        private readonly string _hasPermissionTooltip;
        private readonly bool _enableTooltipsOnDisabledControls;


        public SetTooltipBehavior(
            bool enableTooltipsOnDisabledControls,
            string noPermissionTooltip, 
            string hasPermissionTooltip = null)
        {
            _enableTooltipsOnDisabledControls = enableTooltipsOnDisabledControls;
            _noPermissionTooltip = noPermissionTooltip;
            _hasPermissionTooltip = hasPermissionTooltip;
        }

        public void AlterControl_HasPermission(UIElement element)
        {
            EnDisableTooltipServices(element, _enableTooltipsOnDisabledControls);
            if (!string.IsNullOrEmpty(_hasPermissionTooltip))
            {
                string name = GetElementName(element);
                if (!_backupValues.ContainsKey(name))
                {
                    return;
                }
                string tt = _backupValues.ContainsKey(name) ? _backupValues[name] : null;
                element.SetValue(FrameworkElement.ToolTipProperty, tt);
            }
            else
            {
                element.SetValue(FrameworkElement.ToolTipProperty, _hasPermissionTooltip);
            }
        }

        public void AlterControl_NoPermission(UIElement element)
        {
            EnDisableTooltipServices(element, _enableTooltipsOnDisabledControls);
            BackupCurrentTooltip(element);
            element.SetValue(FrameworkElement.ToolTipProperty, _noPermissionTooltip);
        }

        private string GetElementName(UIElement element)
        {
            string name = element.GetValue(FrameworkElement.NameProperty).ToString();
            if (string.IsNullOrEmpty(name))
            {
                var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
                string newName = $"perm_ctrl_{uid}";
                element.SetValue(FrameworkElement.NameProperty, newName);

                name = element.GetValue(FrameworkElement.NameProperty).ToString();
            }
            return name;
        }

        private void BackupCurrentTooltip(UIElement element)
        {
            string name = GetElementName(element);

            if (!_backupValues.ContainsKey(name))
            {
                object currentTooltip = element.GetValue(FrameworkElement.ToolTipProperty);
                if (currentTooltip != null)
                {
                    _backupValues.Add(name, currentTooltip.ToString());
                }
            }
        }

        private void EnDisableTooltipServices(UIElement element, bool state)
        {
            ToolTipService.SetShowOnDisabled(element, state);
        }
    }
}
