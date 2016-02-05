using System;

namespace Tuner.Wpf.ViewModels
{
    internal interface ISettings
    {
        bool IsValid { get; }
        event EventHandler SettingsChanged;
    }
}