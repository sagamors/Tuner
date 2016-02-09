using System.Collections.ObjectModel;

namespace Tuner.Wpf.Sound
{
    public interface IInstrument
    {
        string Name { set; get; }
        ObservableCollection<IPreset> Presets { set; get; } 
        ObservableCollection<IPreset> FavoritePresets { set; get; }
        IPreset SelectedPreset { set; get; }
    }
}
