namespace Randomizer.Entities
{
    public interface IGateway<T>
    {
        T[] GetAll();
        T GetById(int id);
        void Save(int id);
        int AddNew(T newInstance);
        void Remove(int id);
        int Length { get; }
    }
}