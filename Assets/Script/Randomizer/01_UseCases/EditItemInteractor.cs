using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class EditItemInteractor : IInputPortInteractor<int, string>
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IResponseInteractor _responseInteractor;

        public Action<int, string> InputHandler => Handle;
        private const int Param1Data = 0;
        private const string Param2Data = "";
        Action IInputPortInteractor.InputHandler => () => InputHandler(Param1Data, Param2Data);

        private EditItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IResponseInteractor responseInteractor)
        {
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }

        private void Handle(int itemId, string request)
        {
            var id = _randomizableGateway.ActiveId;
            var randomizable = _randomizableGateway.GetById(id);
            randomizable.Items[itemId].Name = request;
            _randomizableGateway.Save(id);
            
            _responseInteractor.RespondDisplayRandomizable(id);
        }
    }
}