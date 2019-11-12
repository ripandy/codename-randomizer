using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class ResetInputController : BaseInputController
    {
        protected ResetInputController(IInputPortInteractor inputPortInteractor) : base(inputPortInteractor)
        {
        }
    }
}