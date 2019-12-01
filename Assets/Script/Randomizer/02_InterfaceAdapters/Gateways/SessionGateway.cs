using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class SessionGateway : IInitializable
    {
        private readonly IDataStore<UserPreferencesData> _dataStore;
        private readonly IInputPortInteractor<int> _sessionLoaderInteractor;
        
        private SessionGateway(
            IDataStore<UserPreferencesData> dataStore,
            IInputPortInteractor<int> sessionLoaderInteractor)
        {
            _dataStore = dataStore;
            _sessionLoaderInteractor = sessionLoaderInteractor;
        }

        public void Initialize()
        {
            _sessionLoaderInteractor.InputHandler(_dataStore[0].RandomizableId);
        }
    }
}