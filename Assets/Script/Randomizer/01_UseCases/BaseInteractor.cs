using System;
using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public abstract class BaseInteractor : IDisposable
    {
        protected readonly IRequestInteractor RequestInteractor;
        protected readonly IResponseInteractor ResponseInteractor;
        protected readonly IGateway<Label> LabelGateway;
        protected readonly IGateway<Randomizable> RandomizableGateway;

        protected BaseInteractor(
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor,
            IGateway<Label> labelGateway,
            IGateway<Randomizable> randomizableGateway)
        {
            RequestInteractor = requestInteractor;
            ResponseInteractor = responseInteractor;
            LabelGateway = labelGateway;
            RandomizableGateway = randomizableGateway;
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
                case RequestType.PickLabelNavigate:
                case RequestType.MenuNavigate:
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
                case RequestType.PickLabel:
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

        protected void RespondRandomizable(int randomizableId)
        {
            var randomizable = RandomizableGateway.GetById(randomizableId);
            RespondRandomizable(randomizable);
        }

        protected void RespondRandomizable(Randomizable randomizable)
        {
            var items = randomizable.Items.Select(item => item.Name).ToArray();
            var labels = randomizable.LabelIds.Select(id => LabelGateway.GetById(id).Name).ToArray();
            ResponseInteractor.Response(new RandomizableResponseMessage(randomizable.Name, items, labels));
        }

        protected void RespondPickLabel()
        {
            var labels = LabelGateway.GetAll().Select(label => label.Name).ToArray();
            var randomizable = RandomizableGateway.GetActive();
            var labelIds = randomizable.LabelIds;
            ResponseInteractor.Response(new PickLabelListResponseMessage(randomizable.Name, labels, labelIds));
        }
    }
}