using System;
using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class InputController : IInitializable
    {
        public bool IsInitialized { get; private set; }
        
        private readonly IList<IRequestInteractor> _inputPortInteractors;
        private readonly IList<IActionHandler> _actionHandlers;
        private readonly IList<TypeCode> _types;

        protected InputController (
            IList<IRequestInteractor> inputPortInteractors,
            IList<IActionHandler> actionHandlers,
            IList<TypeCode> types)
        {
            _inputPortInteractors = inputPortInteractors;
            _actionHandlers = actionHandlers;
            _types = types;
        }

        public void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
            for (int i = 0; i < _types.Count; i++)
            {
                if (_types[i] == TypeCode.Empty)
                    ConnectAction(i);
                else if (_types[i] == TypeCode.Int32)
                    ConnectAction<int>(i);
                else if (_types[i] == TypeCode.String)
                    ConnectAction<string>(i);
            }
        }

        private IRequestInteractor GetInputPortInteractor<T>(int idx)
        {
            return (IRequestInteractor) _inputPortInteractors[idx];
        }
        
        private IActionHandler<T> GetActionHandler<T>(int idx)
        {
            return (IActionHandler<T>) _actionHandlers[idx];
        }

        private void ConnectAction<T>(int idx)
        {
            var act = GetActionHandler<T>(idx);
            var ip = GetInputPortInteractor<T>(idx);
            act.OnAction = ip.HandleRequest;
        }

        private void ConnectAction(int idx)
        {
            var act = _actionHandlers[idx];
            var ip = _inputPortInteractors[idx];
            act.OnAction = ip.HandleRequest;
        }
    }
}