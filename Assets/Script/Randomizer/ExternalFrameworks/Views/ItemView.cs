using Randomizer.InterfaceAdapters;
using TMPro;
using UnityEngine;
using Zenject;

namespace Randomizer.ExternalFrameworks.Views
{
    public class ItemView : ZenjectProducableObject, IItemView
    {
        private readonly Vector3 _defaultPosition = new Vector3(540f, -140f, 0f);
        private const float DefaultDistanceBetweenObject = -120f;
        
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
            var position = _defaultPosition;
                position.y += DefaultDistanceBetweenObject * order;
            transform.localPosition = position;
        }
        
        public class Factory : PlaceholderFactory<ItemView>
        {
        }
    }
}