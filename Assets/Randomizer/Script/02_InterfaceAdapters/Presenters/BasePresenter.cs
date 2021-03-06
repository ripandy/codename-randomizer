using System;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public abstract class BasePresenter : IInitializable, IDisposable
    {
        public bool IsInitialized { get; private set; }
        
        public DisplayState DisplayState { get; private set; }

        private readonly IResponseInteractor _responseInteractor;

        protected BasePresenter(IResponseInteractor responseInteractor)
        {
            _responseInteractor = responseInteractor;
        }

        public virtual void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
            _responseInteractor.OnResponse += OnResponse;
        }

        public virtual void Dispose()
        {
            _responseInteractor.OnResponse -= OnResponse;
        }

        private void OnResponse(IResponseMessage responseMessage)
        {
            if (responseMessage == null) return;

            SetResultState(responseMessage);
            PreResponse();

            switch (responseMessage)
            {
                case PickLabelListResponseMessage pickLabelListResponseMessage:
                    OnResponse(pickLabelListResponseMessage);
                    break;
                case RandomizableResponseMessage randomizableResponseMessage:
                    OnResponse(randomizableResponseMessage);
                    break;
                case LoadMenuResponseMessage loadMenuResponseMessage:
                    OnResponse(loadMenuResponseMessage);
                    break;
                case ItemListResponseMessage itemListResponseMessage:
                    OnResponse(itemListResponseMessage);
                    break;
            }
            PostResponse();
        }

        private void SetResultState(IResponseMessage responseMessage)
        {
            DisplayState = (DisplayState) responseMessage.ResponseType;
            if (responseMessage.ResponseType == ResponseType.DisplayResult)
                DisplayState = ((ResultResponseMessage) responseMessage).ItemCount > 1
                    ? DisplayState.DisplayResults
                    : DisplayState.DisplayResult;
        }

        protected virtual void PreResponse() { }
        protected virtual void OnResponse(RandomizableResponseMessage responseMessage) { }
        protected virtual void OnResponse(PickLabelListResponseMessage responseMessage) { }
        protected virtual void OnResponse(LoadMenuResponseMessage responseMessage) { }
        protected virtual void OnResponse(ItemListResponseMessage responseMessage) { }
        protected virtual void PostResponse() { }
    }
}