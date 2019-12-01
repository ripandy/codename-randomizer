using System;

namespace Randomizer.UseCases
{
    public class ResetInteractor : IInputPortInteractor
    {
        private readonly IOutputPortInteractor<RandomizeResponseMessage> _randomizeResponseInteractor;
        
        public Action InputHandler => Handle;

        private ResetInteractor(IOutputPortInteractor<RandomizeResponseMessage> randomizeResponseInteractor)
        {
            _randomizeResponseInteractor = randomizeResponseInteractor;
        }

        private void Handle()
        {
            var response = new RandomizeResponseMessage {Success = false, Message = "Reset Randomizer.."};
            _randomizeResponseInteractor.OutputHandler.Invoke(response);
        }
    }
}