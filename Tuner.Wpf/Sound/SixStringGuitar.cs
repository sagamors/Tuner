using System.Collections.ObjectModel;
using Tuner.Core;

namespace Tuner.Wpf.Sound
{
    internal class SixStringGuitar : InstrumentBase
    {
        public SixStringGuitar(INoteFactory noteFactory)
        {
            Name = "6-String Guitar";
            Presets = new ObservableCollection<IPreset>();

            var standartPreset = CreateAndAddPreset("Standard", new ObservableCollection<INote>
            {
                noteFactory.CreateNote(eNote.E, 4),
                noteFactory.CreateNote(eNote.B, 3),
                noteFactory.CreateNote(eNote.G, 3),
                noteFactory.CreateNote(eNote.D, 3),
                noteFactory.CreateNote(eNote.A, 2),
                noteFactory.CreateNote(eNote.E, 2)
            });
            standartPreset.IsFavorite = true;

            var dropD = CreateAndAddPreset("Drop D", new ObservableCollection<INote>
            {
                noteFactory.CreateNote(eNote.E, 4),
                noteFactory.CreateNote(eNote.B, 3),
                noteFactory.CreateNote(eNote.G, 3),
                noteFactory.CreateNote(eNote.D, 3),
                noteFactory.CreateNote(eNote.A, 2),
                noteFactory.CreateNote(eNote.D, 2)
            });

            var dropC = CreateAndAddPreset("Drop C", new ObservableCollection<INote>
            {
                noteFactory.CreateNote(eNote.D, 4),
                noteFactory.CreateNote(eNote.A, 3),
                noteFactory.CreateNote(eNote.F, 3),
                noteFactory.CreateNote(eNote.C, 3),
                noteFactory.CreateNote(eNote.G, 2),
                noteFactory.CreateNote(eNote.C, 2)
            });
            dropC.IsFavorite = true;

            DefaultSelect();
        }

        private IPreset CreateAndAddPreset(string name, ObservableCollection<INote> notes)
        {
            var preset = new Preset()
            {
                Name = name,
                Notes = notes
            };
            Presets.Add(preset);
            return preset;
        }
    }
}