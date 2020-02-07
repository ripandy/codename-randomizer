using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public abstract class BaseController : IInitializable
    {
        protected readonly IRequestInteractor RequestInteractor;
        protected readonly IList<IActionHandler> ActionHandlers;
        protected IActionHandler ActionHandler => ActionHandlers.Count > 0 ? ActionHandlers[0] : null;
        
        public bool IsInitialized { get; private set; }

        protected BaseController(
            IRequestInteractor requestInteractor,
            IList<IActionHandler> actionHandlers)
        {
            RequestInteractor = requestInteractor;
            ActionHandlers = actionHandlers;
        }
        
        protected BaseController(
            IRequestInteractor requestInteractor,
            IActionHandler actionHandler)
        {
            RequestInteractor = requestInteractor;
            ActionHandlers = new List<IActionHandler> { actionHandler };
        }
        
        public virtual void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
            BindAction();
        }

        protected abstract void BindAction();
    }
}