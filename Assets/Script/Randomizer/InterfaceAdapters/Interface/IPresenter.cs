namespace Randomizer.InterfaceAdapters
{
    public interface IPresenter<out T>
    {
        T ViewModelObject { get; }
        bool Visible { get; set; }
    }
}