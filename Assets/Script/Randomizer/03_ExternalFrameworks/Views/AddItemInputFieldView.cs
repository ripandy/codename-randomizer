using Randomizer.InterfaceAdapters;

namespace Randomizer.ExternalFrameworks.Views
{
    public class AddItemInputFieldView : BaseView, IOrderedView
    {
        private int _order;

        public int Order
        {
            get => _order;
            set
            {
                _order = value;
                AdjustPosition(_order);
            }
        }

        private void AdjustPosition(int order)
        {
            transform.SetSiblingIndex(order);
        }
    }
}