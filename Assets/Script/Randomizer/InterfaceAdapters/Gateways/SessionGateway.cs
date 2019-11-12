using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class SessionGateway : IInitializable
    {
        private readonly IDataStore<UserPreferencesData> _dataStore;
        private readonly IInputPortInteractor<LoadSessionRequestMessage> _sessionLoaderInteractor;
        
        private SessionGateway(
            IDataStore<UserPreferencesData> dataStore,
            IInputPortInteractor<LoadSessionRequestMessage> sessionLoaderInteractor)
        {
            _dataStore = dataStore;
            _sessionLoaderInteractor = sessionLoaderInteractor;
        }

        public void Initialize()
        {
            _sessionLoaderInteractor.Handle(new LoadSessionRequestMessage {CurrentSessionId = _dataStore[0].RandomizableId});
        }
    }
}