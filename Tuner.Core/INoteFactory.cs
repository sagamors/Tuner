namespace Tuner.Core
{
    public enum eNote : uint
    {
        C = 0x0,
        Cdies = 0x1,
        D = 0x2,
        Ddies = 0x3,
        E = 0x4,
        F = 0x5,
        Fdies = 0x6,
        G = 0x7,
        Gdies = 0x8,
        A = 0x9,
        Adies = 0xA,
        B = 0xB
    }

    public interface INoteFactory
    {
        double MainFrequency { get; }
        INote CreateNote(uint indexNote, uint octave);
        INote CreateNote(eNote note, uint octave);
    }
}
