using Randomizer.ExternalFrameworks.Views;
using Randomizer.InterfaceAdapters;

namespace Randomizer.ExternalFrameworks.Factories
{
    public class ItemFactory : IFactoryHandler<IItemView>
    {
        private readonly ItemView.Factory _factory;

        private ItemFactory(ItemView.Factory factory)
        {
            _factory = factory;
        }

        public IItemView Create()
        {
            var item = _factory.Create();
            return item;
        }
    }
}