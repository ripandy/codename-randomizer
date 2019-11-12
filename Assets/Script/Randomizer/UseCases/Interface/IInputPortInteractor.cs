namespace Randomizer.UseCases
{
    public interface IInputPortInteractor
    {
        void Handle();
    }
    
    public interface IInputPortInteractor<in T>
    {
        void Handle(T request);
    }
}