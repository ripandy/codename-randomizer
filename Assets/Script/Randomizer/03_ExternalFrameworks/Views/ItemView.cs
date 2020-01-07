using Randomizer.InterfaceAdapters;
using TMPro;
using UnityEngine;
using Zenject;

namespace Randomizer.ExternalFrameworks.Views
{
    public class ItemView : ZenjectProducableObject, IItemView
    {
        [SerializeField] private TMP_Text text;
        
        private int _order;
        public int Order
        {
            get => _order;
            set
            {
                _order = value;
                ArrangeByOrder(_order);
            }
        }

        public string Text
        {
            get => text.text;
            set => text.text = value;
        }

        private void ArrangeByOrder(int order)
        {
            transform.SetSiblingIndex(order);
        }
        
        public class Factory : PlaceholderFactory<ItemView>
        {
        }
    }
}