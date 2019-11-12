using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class ClearItemInputController : BaseInputController
    {
        protected ClearItemInputController(IInputPortInteractor inputPortInteractor) : base(inputPortInteractor)
        {
        }
    }
}