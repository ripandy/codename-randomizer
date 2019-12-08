using System;

namespace Randomizer.UseCases
{
    public class ResetInteractor : IInputPortInteractor
    {
        private readonly IOutputPortInteractor _responseInteractor;
        
        public Action InputHandler => Handle;

        private ResetInteractor(IOutputPortInteractor responseInteractor)
        {
            _responseInteractor = responseInteractor;
        }

        private void Handle()
        {
            _responseInteractor.ResponseType = ResponseType.DisplayRandomizable;
            _responseInteractor.RaiseResponseEvent();
        }
    }
}