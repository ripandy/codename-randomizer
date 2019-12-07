using System;
using Randomizer.Entities;
using Randomizer.UseCases;
using UnityEngine;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class SessionGateway : IInitializable, IDisposable
    {
        private readonly Session _session;
        private readonly IDataStore<UserPreferencesData> _dataStore;
        private readonly IInputPortInteractor<int> _sessionLoaderInteractor;
        
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
            _sessionLoaderInteractor.InputHandler(_dataStore[0].RandomizableId);
        }

        public void Dispose()
        {
            Debug.Log("disposed...");
            _dataStore[0].RandomizableId = _session.ActiveRandomizableId;
        }
    }
}