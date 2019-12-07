using System;
using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class InputController : IInitializable
    {
        private readonly IList<IInputPortInteractor> _inputPortInteractors;
        private readonly IList<IActionHandler> _actionHandlers;
        private readonly IList<TypeCode> _types;

        protected InputController (
            IList<IInputPortInteractor> inputPortInteractors,
            IList<IActionHandler> actionHandlers,
            IList<TypeCode> types)
        {
            _inputPortInteractors = inputPortInteractors;
            _actionHandlers = actionHandlers;
            _types = types;
        }

        public void Initialize()
        {
            for (int i = 0; i < _types.Count; i++)
            {
                if (_types[i] == TypeCode.Empty)
                    ConnectAction(i);
                else if (_types[i] == TypeCode.String)
                    ConnectAction<string>(i);
            }
        }

        private IInputPortInteractor<T> GetInputPortInteractor<T>(int idx)
        {
            return (IInputPortInteractor<T>) _inputPortInteractors[idx];
        }
        
        private IActionHandler<T> GetActionHandler<T>(int idx)
        {
            return (IActionHandler<T>) _actionHandlers[idx];
        }

        private void ConnectAction<T>(int idx)
        {
            var act = GetActionHandler<T>(idx);
            var ip = GetInputPortInteractor<T>(idx);
            act.OnAction = ip.InputHandler;
        }

        private void ConnectAction(int idx)
        {
            var act = _actionHandlers[idx];
            var ip = _inputPortInteractors[idx];
            act.OnAction = ip.InputHandler;
        }
    }
}