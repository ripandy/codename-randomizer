namespace Randomizer.UseCases
{
    public interface IResponseInteractor
    {
        ResponseType ResponseType { get; }
        void RespondDisplayResult(string[] resultValues, string title = "Result");
        void RespondDisplayRandomizable(int randomizableId);
        void RespondDisplayLabel(int labelId);
    }
}