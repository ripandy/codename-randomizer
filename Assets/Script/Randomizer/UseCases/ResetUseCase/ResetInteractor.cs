using Randomizer.UseCases;

namespace Script.Randomizer.UseCases.ResetUseCase
{
    public class ResetInteractor : IInputPortInteractor
    {
        private readonly IOutputPortInteractor<ResetResponseMessage> _resetOutputPortInteractor;

        private ResetInteractor(IOutputPortInteractor<ResetResponseMessage> resetOutputPortInteractor)
        {
            _resetOutputPortInteractor = resetOutputPortInteractor;
        }
        
        public void Handle()
        {
            _resetOutputPortInteractor.Handle(new ResetResponseMessage { Success = true });
        }
    }
}