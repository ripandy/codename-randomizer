namespace Randomizer.InterfaceAdapters
{
    public interface IDataStore<out T>
    {
        T this[int index] { get; }
        T[] Data { get; }
    }
}