using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class RandomizeInteractor : IInputPortInteractor
    {
        private readonly Session _session;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor<RandomizeResponseMessage> _randomizeResponseInteractor;

        public Action InputHandler => Handle;

        private RandomizeInteractor(
            Session session,
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor<RandomizeResponseMessage> randomizeResponseInteractor)
        {
            _session = session;
            _randomizableGateway = randomizableGateway;
            _randomizeResponseInteractor = randomizeResponseInteractor;
        }
        
        private void Handle()
        {
            var randomizable = _randomizableGateway.GetById(_session.ActiveRandomizableId);
            var success = randomizable.ItemCount > 1;
            var response = new RandomizeResponseMessage { Success = success };
            
            if (success)
            {
                var item = randomizable.Randomize();
                response.Message = item.Name;
            }
            else
            {
                response.Message = "Not enough item to Randomize..";
            }
                
            _randomizeResponseInteractor.OutputHandler.Invoke(response);
        }
        
        public class RandomizeResponseInteractor : IOutputPortInteractor<RandomizeResponseMessage>
        {
            public Action<RandomizeResponseMessage> OutputHandler { get; set; }
        }
    }
}