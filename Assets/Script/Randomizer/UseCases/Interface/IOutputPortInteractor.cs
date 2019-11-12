namespace Randomizer.UseCases
{
    public interface IOutputPortInteractor<in T>
    {
        void Handle(T response);
    }
}