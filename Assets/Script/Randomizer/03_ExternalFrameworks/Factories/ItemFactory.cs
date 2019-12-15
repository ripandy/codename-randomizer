using System.Collections.Generic;
using Randomizer.ExternalFrameworks.Views;
using Randomizer.InterfaceAdapters;

namespace Randomizer.ExternalFrameworks.Factories
{
    public class ItemFactory : IFactoryHandler<IItemView>
    {
        private readonly IList<ItemView.Factory> _factories;

        private ItemFactory(IList<ItemView.Factory> factories)
        {
            _factories = factories;
        }

        public IItemView Create(int variant)
        {
            var item = _factories[variant].Create();
            return item;
        }
    }
}