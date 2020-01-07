using System;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public abstract class BasePresenter : IInitializable, IDisposable
    {
        public bool IsInitialized { get; private set; }
        
        protected readonly IOutputPortInteractor ResponseInteractor;

        protected BasePresenter(IOutputPortInteractor responseInteractor)
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

        protected virtual void OnResponse()
        {
            var responseType = ResponseInteractor.ResponseType;
            switch (responseType)
            {
                case ResponseType.DisplayResult:
                    OnDisplayResultResponse();
                    break;
                case ResponseType.DisplayRandomizable:
                    OnDisplayRandomizableResponse();
                    break;
                case ResponseType.DisplayGroup:
                    OnDisplayGroupResponse();
                    break;
            }
        }

        protected virtual void OnDisplayResultResponse() { }
        protected virtual void OnDisplayRandomizableResponse() { }
        protected virtual void OnDisplayGroupResponse() { }
    }
}