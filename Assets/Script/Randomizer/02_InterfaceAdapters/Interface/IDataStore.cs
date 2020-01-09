namespace Randomizer.InterfaceAdapters
{
    public interface IDataStore<out T> where T : class
    {
        int ActiveIndex { get; set; }
        T this[int index] { get; }
        T[] Data { get; }
        T ActiveData { get; }
        int Create();
        void Delete(int index);
    }
}