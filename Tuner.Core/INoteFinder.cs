namespace Tuner.Core
{
    public interface INoteFinder
    {
        INoteFactory NoteFactory { get; }
        INote FindNearestNote(double frequency);
    }
}
