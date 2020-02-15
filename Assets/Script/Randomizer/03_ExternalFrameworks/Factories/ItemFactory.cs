using System.Collections.Generic;
using Randomizer.ExternalFrameworks.Views;
using Randomizer.InterfaceAdapters;

namespace Randomizer.ExternalFrameworks.Factories
{
    public class ItemFactory : IFactoryHandler<IItemView>
    {
        private readonly IList<ItemView.Factory> _factories;
        private readonly PickLabelView.Factory _pickLabelFactory;

        private ItemFactory(
            IList<ItemView.Factory> factories,
            PickLabelView.Factory pickLabelFactory)
        {
            _factories = factories;
            _pickLabelFactory = pickLabelFactory;
        }

        public IItemView Create(int variant)
        {
            IItemView item = variant == (int) ItemType.PickLabelList
                ? _pickLabelFactory.Create()
                : _factories[variant].Create();
            return item;
        }
    }
}