using System.Collections.ObjectModel;
using Tuner.Core;

namespace Tuner.Wpf.Sound
{
    internal class SixStringGuitar : InstrumentBase
    {
        public SixStringGuitar(INoteFactory noteFactory)
        {
            Name = "6-String Guitar";
            var standartPreset = new Preset()
            {
                Name = "Standard",
                Notes = new ObservableCollection<INote>
                {
                    noteFactory.CreateNote(eNote.E, 4),
                    noteFactory.CreateNote(eNote.B, 3),
                    noteFactory.CreateNote(eNote.G, 3),
                    noteFactory.CreateNote(eNote.D, 3),
                    noteFactory.CreateNote(eNote.A, 2),
                    noteFactory.CreateNote(eNote.E, 2)
                }
            };
            Presets = new ObservableCollection<IPreset> { standartPreset };
            FavoritePresets = new ObservableCollection<IPreset>
            {
                standartPreset
            };

            DefaultSelect();
        }
    }
}