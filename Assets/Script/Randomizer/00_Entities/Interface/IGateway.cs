namespace Randomizer.Entities
{
    public interface IGateway<T>
    {
        int ActiveId { get; set; }
        T[] GetAll();
        T GetById(int id);
        T GetActive();
        void Save(int id);
        int AddNew(T newInstance);
        void Remove(int id);
    }
}