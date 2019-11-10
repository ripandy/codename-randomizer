namespace Randomizer.UseCases
{
    public interface IInputPortInteractor<in T>
    {
        void Handle(T request);
    }
}