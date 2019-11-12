namespace Randomizer.InterfaceAdapters
{
    public interface IActionHandler
    {
        void Handle();
    }
    
    public interface IActionHandler<in TParams>
    {
        void Handle(TParams args);
    }
}