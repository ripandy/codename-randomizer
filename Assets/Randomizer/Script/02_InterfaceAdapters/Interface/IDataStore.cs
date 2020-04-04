namespace Randomizer.InterfaceAdapters
{
    public interface IDataStore<out T> where T : class
    {
        int ActiveId { get; set; }
        T this[int id] { get; }
        T[] Data { get; }
        void Create(int id);
        void Delete(int id);
        void SaveToJson();
        void LoadFromJson();
    }
}