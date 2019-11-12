using Randomizer.InterfaceAdapters;

namespace Randomizer.ExternalFrameworks
{
    public class ItemFactory : IFactoryHandler<IPresenter>
    {
        private readonly ItemView.Factory _factory;

        private ItemFactory(ItemView.Factory factory)
        {
            _factory = factory;
        }

        public IPresenter Create()
        {
            var item = _factory.Create();
            return item.Presenter;
        }
    }
}