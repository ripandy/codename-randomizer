using Randomizer.InterfaceAdapters;

namespace Randomizer.ExternalFrameworks
{
    public class ItemFactoryHandler : IFactoryHandler<IPresenter>
    {
        private readonly ZenjectProducableObject.Factory _factory;

        private ItemFactoryHandler(ZenjectProducableObject.Factory factory)
        {
            _factory = factory;
        }

        public IPresenter Create()
        {
            var item = _factory.Create() as ItemViewer;
            return item == null ? null : item.Presenter;
        }
    }
}