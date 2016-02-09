using System.Collections.ObjectModel;
using System.Linq;
using Tuner.Wpf.Core;

namespace Tuner.Wpf.Sound
{
    class InstrumentBase : NotificationBase, IInstrument
    {
        public string Name { get; set; }
        public ObservableCollection<IPreset> Presets { get; set; } 
        public ObservableCollection<IPreset> FavoritePresets { get; set; }
        public IPreset SelectedPreset { get; set; }

        protected void DefaultSelect()
        {
            if (FavoritePresets != null)
                SelectedPreset = FavoritePresets.FirstOrDefault();
            if (SelectedPreset == null && Presets != null)
                SelectedPreset = Presets.FirstOrDefault();
        }
    }
}