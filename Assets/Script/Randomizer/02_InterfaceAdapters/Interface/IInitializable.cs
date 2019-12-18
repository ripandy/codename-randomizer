namespace Randomizer.InterfaceAdapters
{
    public interface IInitializable
    {
        bool IsInitialized { get; }
        void Initialize();
    }
}