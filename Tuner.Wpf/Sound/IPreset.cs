using System.Collections.ObjectModel;
using Tuner.Core;

namespace Tuner.Wpf.Sound
{
    public interface IPreset
    {
        string Name { set; get; }
        ObservableCollection<INote> Notes { set; get; }  
    }
}