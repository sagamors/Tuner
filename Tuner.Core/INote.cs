namespace Tuner.Core
{
    public interface INote
    {
        double Frequency { get; }
        string Name { get; }
        uint Octave { get; }
        string FullName { get; }
    }
}
