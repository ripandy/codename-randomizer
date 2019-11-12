namespace Randomizer.Entity
{
    public interface IGateway<out T>
    {
        T this[int index] { get; }
        T GetById(int id);
//        void Save(int id);
    }
}