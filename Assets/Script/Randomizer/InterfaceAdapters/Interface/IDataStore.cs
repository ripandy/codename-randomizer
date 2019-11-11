namespace Randomizer.InterfaceAdapters
{
    public interface IDataStore<out T>
    {
        T[] Data { get; }
    }
}