using System;
using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public abstract class BaseInteractor : IDisposable
    {
        protected readonly IRequestInteractor RequestInteractor;
        protected readonly IResponseInteractor ResponseInteractor;
        protected readonly IGateway<Randomizable> RandomizableGateway;
        protected readonly IGateway<Label> LabelGateway;
        protected readonly IGateway<Item> ItemGateway;


        protected BaseInteractor(
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor,
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IGateway<Item> itemGateway)
        {
            RequestInteractor = requestInteractor;
            ResponseInteractor = responseInteractor;
            RandomizableGateway = randomizableGateway;
            LabelGateway = labelGateway;
            ItemGateway = itemGateway;
            RequestInteractor.OnRequest += OnRequest;
        }

        public virtual void Dispose()
        {
            RequestInteractor.OnRequest -= OnRequest;
        }

        private void OnRequest(IRequestMessage requestMessage)
        {
            switch (requestMessage)
            {
                case null:
                    return;
                case EditItemRequestMessage editItemRequestMessage:
                    OnRequest(editItemRequestMessage);
                    break;
                case EditLabelRequestMessage editLabelRequestMessage:
                    OnRequest(editLabelRequestMessage);
                    break;
                case LoadRandomizableRequestMessage loadRandomizableRequestMessage:
                    OnRequest(loadRandomizableRequestMessage);
                    break;
                case LoadLabelRequestMessage loadLabelRequestMessage:
                    OnRequest(loadLabelRequestMessage);
                    break;
                case RequestMessage<string> stringRequestMessage:
                    OnRequest(stringRequestMessage);
                    break;
                case RequestMessage<int> intRequestMessage:
                    OnRequest(intRequestMessage);
                    break;
                case RequestMessage plainRequestMessage:
                    OnRequest(plainRequestMessage);
                    break;
            }
        }

        protected virtual void OnRequest(RequestMessage requestMessage) { }
        protected virtual void OnRequest(RequestMessage<int> requestMessage) { }
        protected virtual void OnRequest(RequestMessage<string> requestMessage) { }
        protected virtual void OnRequest(EditItemRequestMessage requestMessage) { }
        protected virtual void OnRequest(EditLabelRequestMessage requestMessage) { }
        protected virtual void OnRequest(LoadLabelRequestMessage requestMessage) { }
        protected virtual void OnRequest(LoadRandomizableRequestMessage requestMessage) { }

        protected void RespondRandomizable(Randomizable randomizable)
        {
            var items = randomizable.ItemIds.Select(id => ItemGateway.GetById(id).Name).ToArray();
            var labels = randomizable.LabelIds.Select(id => LabelGateway.GetById(id).Name).ToArray();
            ResponseInteractor.Response(new RandomizableResponseMessage(randomizable.Name, items, labels));
        }

        protected void RespondPickLabel()
        {
            var labels = LabelGateway.GetAll().Select(label => label.Name).ToArray();
            var randomizable = RandomizableGateway.Active;
            var labelIds = randomizable.LabelIds;
            ResponseInteractor.Response(new PickLabelListResponseMessage(randomizable.Name, labels, labelIds));
        }

        protected void RespondManageLabel()
        {
            var labels = LabelGateway.GetAll().Select(label => label.Name).ToArray();
            ResponseInteractor.Response(new ItemListResponseMessage(ResponseType.DisplayManageLabel, "", labels));
        }
    }
}