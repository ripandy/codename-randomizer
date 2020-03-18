namespace Randomizer.InterfaceAdapters
{
    public interface IFactoryHandler<out T>
    {
        T Create(int variant = 0);
    }
}