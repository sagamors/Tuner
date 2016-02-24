using System;
using System.Collections.ObjectModel;
using Tuner.Core;

namespace Tuner.Wpf.Sound
{
    public interface IPreset : ICloneable
    {
        string Name { set; get; }
        ObservableCollection<INote> Notes {set; get; }  
        bool IsFavorite { set; get; }
        void CloneTo(IPreset preset);
    }
}