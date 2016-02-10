using System.Collections.ObjectModel;
using System.Linq;
using Tuner.Wpf.Core;

namespace Tuner.Wpf.Sound
{
    class InstrumentBase : NotificationBase, IInstrument
    {
        public string Name { get; set; }
        public ObservableCollection<IPreset> Presets { get; set; } 
        public IPreset SelectedPreset { get; set; }

        protected void DefaultSelect()
        {
            SelectedPreset = Presets.FirstOrDefault(preset => preset.IsFavorite);
            if (SelectedPreset == null && Presets != null)
                SelectedPreset = Presets.FirstOrDefault();
        }
    }
}