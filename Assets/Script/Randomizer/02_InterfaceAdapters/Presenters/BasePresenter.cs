using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class BasePresenter : IInitializable
    {
        private readonly IOutputPortInteractor<AddItemResponseMessage> _addItemResponse;
        private readonly IOutputPortInteractor<RandomizeResponseMessage> _randomizeResponse;
        private readonly IOutputPortInteractor<ReloadResponseMessage> _reloadResponse;

        protected BasePresenter(IList<IOutputPortInteractor> responses)
        {
            _addItemResponse = (IOutputPortInteractor<AddItemResponseMessage>) responses[0];
            _randomizeResponse = (IOutputPortInteractor<RandomizeResponseMessage>) responses[1];
            _reloadResponse = (IOutputPortInteractor<ReloadResponseMessage>) responses[2];
        }

        public virtual void Initialize()
        {
            _addItemResponse.OutputHandler += OnAddItem;
            _randomizeResponse.OutputHandler += OnRandomize;
            _reloadResponse.OutputHandler += OnReload;
        }

        protected virtual void OnAddItem(AddItemResponseMessage responseMessage)
        {
        }

        protected virtual void OnRandomize(RandomizeResponseMessage responseMessage)
        {
        }

        protected virtual void OnReload(ReloadResponseMessage responseMessage)
        {
        }
    }
}