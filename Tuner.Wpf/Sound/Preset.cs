using System;
using System.Collections.ObjectModel;
using Tuner.Core;
using Tuner.Wpf.Core;

namespace Tuner.Wpf.Sound
{
    public class Preset : NotificationBase, IPreset, ICloneable
    {
        public string Name { get; set; }
        public ObservableCollection<INote> Notes { get; set; }
        public bool IsFavorite { get; set; }

        public Preset()
        {
            Notes = new ObservableCollection<INote>();
        }

        public object Clone()
        {
            var preset = new Preset();
            CloneTo(preset);
            return preset;
        }

        public void CloneTo(IPreset preset)
        {
            preset.Name = this.Name;
            preset.Notes = this.Notes == null ? null : new ObservableCollection<INote>(this.Notes);
            preset.IsFavorite = this.IsFavorite;
        }
    }
}
