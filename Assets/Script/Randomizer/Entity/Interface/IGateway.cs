namespace Randomizer.Entity
{
    public interface IGateway<out T>
    {
        T GetById(int id);
        void Save(int id);
    }
}