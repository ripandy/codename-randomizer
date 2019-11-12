using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class BaseInputController : IActionHandler
    {
        private IInputPortInteractor _inputPortInteractor;

        protected BaseInputController (IInputPortInteractor inputPortInteractor)
        {
            _inputPortInteractor = inputPortInteractor;
        }
        
        public void Handle()
        {
            _inputPortInteractor.Handle();
        }
    }
}