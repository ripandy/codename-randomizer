using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class RandomizeInputController : IActionHandler
    {
        private readonly IInputPortInteractor _randomizeRequestHandler;
        
        private RandomizeInputController(IInputPortInteractor randomizeRequestHandler)
        {
            _randomizeRequestHandler = randomizeRequestHandler;
        }
        
        public void Handle()
        {
            _randomizeRequestHandler.Handle();
        }
    }
}