using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class SessionGateway : IInitializable
    {
        public bool IsInitialized { get; private set; }
        
        private readonly IDataStore<RandomizableData> _dataStore;
        private readonly IInputPortInteractor<int> _sessionLoaderInteractor;

        private SessionGateway(
            IDataStore<RandomizableData> dataStore,
            IInputPortInteractor<int> sessionLoaderInteractor)
        {
            _dataStore = dataStore;
            _sessionLoaderInteractor = sessionLoaderInteractor;
        }

        public void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
            var param = _dataStore.ActiveIndex;
            _sessionLoaderInteractor.InputHandler(param);
        }
    }
}