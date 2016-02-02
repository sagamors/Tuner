namespace Tuner.Wpf.Core
{

    public interface IViewModel<TView>
    {
        TView View { get; }
    }
}
