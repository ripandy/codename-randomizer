using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controller
{
    public class RandomizeInputController
    {
        private readonly IInputPortInteractor<RandomizeRequestMessage> _randomizeRequestHandler;
        private readonly IButtonHandler _button;

        private RandomizeInputController(
            IInputPortInteractor<RandomizeRequestMessage> randomizeRequestHandler,
            IButtonHandler button)
        {
            _randomizeRequestHandler = randomizeRequestHandler;
            _button = button;
            
            Initialize();
        }

        private void Initialize()
        {
            _button.Subscribe(RequestRandomize);
        }

        private void RequestRandomize()
        {
            _randomizeRequestHandler.Handle(new RandomizeRequestMessage { RandomizableId = 0 });
        }
    }
}