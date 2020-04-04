namespace Randomizer.InterfaceAdapters
{
    public interface IOrderedView : IView
    {
        int Order { get; set; }
    }
}