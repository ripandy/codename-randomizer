using Randomizer.InterfaceAdapters;
using Zenject;

namespace Randomizer.ExternalFrameworks
{
    public class ItemFactoryHandler : IFactoryHandler<IPresenter<ItemViewModel>>
    {
        private ZenjectProducableObject.Factory _factory;

        [Inject]
        private void Injection(ZenjectProducableObject.Factory factory)
        {
            _factory = factory;
        }
        
        public IPresenter<ItemViewModel> Create()
        {
            var item = _factory?.Create() as ItemViewer;
            return item == null ? null : item.Presenter;
        }
    }
}