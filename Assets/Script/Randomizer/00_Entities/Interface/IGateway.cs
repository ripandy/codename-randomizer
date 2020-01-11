namespace Randomizer.Entities
{
    public interface IGateway<T>
    {
        int ActiveId { get; set; }
        int Length { get; }
        T[] GetAll();
        T GetById(int id);
        T GetActive();
        void Save(int id);
        void SaveActive();
        int AddNew(T newInstance);
        void Remove(int id);
    }
}