namespace Randomizer.InterfaceAdapters
{
    public interface IPresenter
    {
        bool Visible { get; set; }
    }
    
    public interface IPresenter<out T> : IPresenter
    {
        T ViewModelObject { get; }
    }
}