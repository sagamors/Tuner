using System.Collections.ObjectModel;

namespace Tuner.Wpf.Sound
{
    public interface IInstrument
    {
        string Name { set; get; }
        ObservableCollection<IPreset> Presets { get; }
        IPreset SelectedPreset { set; get; }
    }
}
