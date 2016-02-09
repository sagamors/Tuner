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
            var total = FrequencyUtils.GetTotalIndexNote(frequency, NoteFactory.MainFrequency);
            return NoteFactory.CreateNote(FrequencyUtils.GetIndex(total), FrequencyUtils.GetOctave(total));
        }
    }
}
