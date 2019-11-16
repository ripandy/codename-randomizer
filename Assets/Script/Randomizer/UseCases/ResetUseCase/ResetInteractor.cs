namespace Randomizer.UseCases
{
    public class ResetInteractor : IInputPortInteractor
    {
        private readonly IOutputPortInteractor<RandomizeResponseMessage> _resetOutputPortInteractor;

        private ResetInteractor(IOutputPortInteractor<RandomizeResponseMessage> resetOutputPortInteractor)
        {
            _resetOutputPortInteractor = resetOutputPortInteractor;
        }
        
        public void Handle()
        {
            _resetOutputPortInteractor.Handle(new RandomizeResponseMessage { Success = false, Message = "Reset Randomizer.."});
        }
    }
}