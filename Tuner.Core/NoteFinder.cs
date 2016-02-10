namespace Tuner.Core
{
    public class NoteFinder : INoteFinder
    {
        public INoteFactory NoteFactory {private set; get; }

        public NoteFinder(INoteFactory noteFactory)
        {
            NoteFactory = noteFactory;
        }

        public INote FindNearestNote(double frequency)
        {
            var total = NoteHelper.GetTotalIndexNote(frequency, NoteFactory.MainFrequency);
            return NoteFactory.CreateNote(NoteHelper.GetIndex(total), NoteHelper.GetOctave(total));
        }
    }
}
