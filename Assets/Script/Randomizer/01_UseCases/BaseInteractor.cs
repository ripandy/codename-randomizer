using System;
using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public abstract class BaseInteractor : IDisposable
    {
        protected readonly IRequestInteractor RequestInteractor;
        protected readonly IResponseInteractor ResponseInteractor;

        protected BaseInteractor(
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
        {
            RequestInteractor = requestInteractor;
            ResponseInteractor = responseInteractor;
            RequestInteractor.OnRequest += OnRequest;
        }

        public virtual void Dispose()
        {
            RequestInteractor.OnRequest -= OnRequest;
        }

        private void OnRequest(IRequestMessage requestMessage)
        {
            if (requestMessage == null) return;
            
            switch (requestMessage.RequestType)
            {
                case RequestType.AddItem:
                case RequestType.EditTitle:
                    OnRequest(requestMessage as RequestMessage<string>);
                    break;
                case RequestType.AddRandomizable:
                case RequestType.Randomize:
                    OnRequest(requestMessage as RequestMessage);
                    break;
                case RequestType.EditItem:
                    OnRequest(requestMessage as EditItemRequestMessage);
                    break;
                case RequestType.LoadRandomizable:
                    OnRequest(requestMessage as LoadRandomizableRequestMessage);
                    break;
                case RequestType.LoadLabel:
                    OnRequest(requestMessage as LoadLabelRequestMessage);
                    break;
                case RequestType.RemoveItem:
                    OnRequest(requestMessage as RequestMessage<int>);
                    break;
            }
        }

        protected virtual void OnRequest(RequestMessage requestMessage) { }
        protected virtual void OnRequest(RequestMessage<int> requestMessage) { }
        protected virtual void OnRequest(RequestMessage<string> requestMessage) { }
        protected virtual void OnRequest(EditItemRequestMessage requestMessage) { }
        protected virtual void OnRequest(LoadLabelRequestMessage requestMessage) { }
        protected virtual void OnRequest(LoadRandomizableRequestMessage requestMessage) { }

        protected void RespondRandomizable(Randomizable randomizable)
        {
            var items = randomizable.Items.Select(item => item.Name).ToArray();
            ResponseInteractor.Response(new RandomizableResponseMessage(randomizable.Name, items));
        }
    }
}