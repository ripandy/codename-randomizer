namespace Randomizer.Entities
{
    public interface IGateway<T>
    {
        int ActiveId { get; set; }
        T Active { get; }
        T this[int index] { get; }
        T[] GetAll();
        T GetById(int id);
        int GetIndex(int id);
        void Save(int id);
        void AddNew(T newInstance);
        void Remove(int id);
    }
}