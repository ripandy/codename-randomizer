using System;
using Randomizer.Entities;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class SessionGateway : IInitializable, IDisposable
    {
        public bool IsInitialized { get; private set; }
        
        private readonly Session _session;
        private readonly IDataStore<UserPreferencesData> _dataStore;
        private readonly IInputPortInteractor<int> _sessionLoaderInteractor;

        private const int DefaultIndex = 0;
        private UserPreferencesData DefaultData => _dataStore[DefaultIndex];
        
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
            if (IsInitialized) return;
                IsInitialized = true;
            var param = DefaultData.RandomizableId;
            _sessionLoaderInteractor.InputHandler(param);
        }

        public void Dispose()
        {
            DefaultData.GroupId = _session.ActiveLabelId;
            DefaultData.RandomizableId = _session.ActiveRandomizableId;
        }
    }
}