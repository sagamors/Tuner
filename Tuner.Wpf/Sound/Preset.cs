using System.Collections.ObjectModel;
using Tuner.Core;
using Tuner.Wpf.Core;

namespace Tuner.Wpf.Sound
{
    public class Preset : NotificationBase, IPreset
    {
        public string Name { get; set; }
        public ObservableCollection<INote> Notes { get; set; }
        public bool IsFavorite { get; set; }
    }
}
