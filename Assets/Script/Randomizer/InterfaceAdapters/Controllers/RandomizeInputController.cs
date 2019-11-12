using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class RandomizeInputController : BaseInputController
    {
        protected RandomizeInputController(IInputPortInteractor inputPortInteractor) : base(inputPortInteractor)
        {
        }
    }
}