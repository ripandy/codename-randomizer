using System;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public abstract class BasePresenter : IInitializable, IDisposable
    {
        public bool IsInitialized { get; private set; }
        
        protected readonly IResponseInteractor ResponseInteractor;

        protected BasePresenter(IResponseInteractor responseInteractor)
        {
            ResponseInteractor = responseInteractor;
        }

        public virtual void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
            ResponseInteractor.OnResponse += OnResponse;
        }

        public virtual void Dispose()
        {
            ResponseInteractor.OnResponse -= OnResponse;
        }

        protected abstract void OnResponse(IResponseMessage responseMessage);
    }
}