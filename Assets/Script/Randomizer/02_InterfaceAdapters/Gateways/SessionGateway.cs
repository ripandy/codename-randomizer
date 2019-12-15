using System;
using Randomizer.Entities;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class SessionGateway : IInitializable, IDisposable
    {
        private readonly Session _session;
        private readonly IDataStore<UserPreferencesData> _dataStore;
        private readonly IInputPortInteractor<int> _sessionLoaderInteractor;

        private const int DefaultIndex = 0;
        
        private SessionGateway(
            Session session,
            IDataStore<UserPreferencesData> dataStore,
            IInputPortInteractor<int> sessionLoaderInteractor)
        {
            _session = session;
            _dataStore = dataStore;
            _sessionLoaderInteractor = sessionLoaderInteractor;
        }

        public void Initialize()
        {
            _sessionLoaderInteractor.InputHandler(_dataStore[DefaultIndex].RandomizableId);
        }

        public void Dispose()
        {
            _dataStore[0].RandomizableId = _session.ActiveRandomizableId;
        }
    }
}