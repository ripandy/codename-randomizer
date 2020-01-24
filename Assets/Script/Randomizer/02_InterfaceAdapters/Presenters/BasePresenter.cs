using System;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public abstract class BasePresenter : IInitializable, IDisposable
    {
        public bool IsInitialized { get; private set; }
        
        public DisplayState DisplayState { get; private set; }
        
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

        private void OnResponse(IResponseMessage responseMessage)
        {
            if (responseMessage == null) return;

            DisplayState = (DisplayState) responseMessage.ResponseType;
            switch (responseMessage.ResponseType)
            {
                case ResponseType.DisplayResult:
                    AdjustResultState(responseMessage as ResultResponseMessage);
                    OnResponse(responseMessage as ResultResponseMessage);
                    break;
                case ResponseType.DisplayRandomizable:
                    OnResponse(responseMessage as RandomizableResponseMessage);
                    break;
                case ResponseType.DisplayLabel:
                    OnResponse(responseMessage as LabelResponseMessage);
                    break;
            }
        }

        private void AdjustResultState(ResultResponseMessage responseMessage)
        {
            DisplayState = responseMessage.ItemCount > 1 ? DisplayState.DisplayResults : DisplayState.DisplayResult;
        }
        
        protected virtual void OnResponse(ResultResponseMessage responseMessage) { }
        protected virtual void OnResponse(RandomizableResponseMessage responseMessage) { }
        protected virtual void OnResponse(LabelResponseMessage responseMessage) { }
        
    }
}