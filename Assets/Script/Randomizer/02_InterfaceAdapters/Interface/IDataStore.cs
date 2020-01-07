namespace Randomizer.InterfaceAdapters
{
    public interface IDataStore<out T> where T : class
    {
        T this[int index] { get; }
        T[] Data { get; }
        int Create();
        void Delete(int index);
    }
}