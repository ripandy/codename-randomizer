namespace Randomizer.Entities
{
    public interface IGateway<out T>
    {
        T[] GetAll();
        T GetById(int id);
        void Save(int id);
    }
}