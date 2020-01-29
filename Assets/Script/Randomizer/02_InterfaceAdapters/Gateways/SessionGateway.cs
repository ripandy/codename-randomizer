using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class SessionGateway : IInitializable
    {
        public bool IsInitialized { get; private set; }
        
        private readonly IDataStore<RandomizableData> _randomizableDataStore;
        private readonly IDataStore<LabelData> _labelDataStore;
        private readonly IRequestInteractor _requestInteractor;

        private SessionGateway(
            IDataStore<RandomizableData> randomizableDataStore,
            IDataStore<LabelData> labelDataStore,
            IRequestInteractor requestInteractor)
        {
            _randomizableDataStore = randomizableDataStore;
            _labelDataStore = labelDataStore;
            _requestInteractor = requestInteractor;
        }

        public void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
                
            if (_randomizableDataStore.ActiveIndex >= 0)
                _requestInteractor.Request(new LoadRandomizableRequestMessage(_randomizableDataStore.ActiveIndex));
            else
                _requestInteractor.Request(new LoadLabelRequestMessage(_labelDataStore.ActiveIndex));
        }
    }
}