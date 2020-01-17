using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class EditTitleInteractor : IInputPortInteractor<string>
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IResponseInteractor _responseInteractor;

        public Action<string> InputHandler => Handle;
        private const string Data = "";
        Action IInputPortInteractor.InputHandler => () => InputHandler(Data);

        private EditTitleInteractor(
            IGateway<Randomizable> randomizableGateway,
            IResponseInteractor responseInteractor)
        {
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }

        private void Handle(string request)
        {
            var id = _randomizableGateway.ActiveId;
            if (id <= 0) return;
            
            var randomizable = _randomizableGateway.GetById(id);
            randomizable.Name = request;
            _randomizableGateway.Save(id);

            _responseInteractor.RespondDisplayRandomizable(id);
        }
    }
}