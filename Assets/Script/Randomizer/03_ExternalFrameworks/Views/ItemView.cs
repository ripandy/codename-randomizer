using Randomizer.InterfaceAdapters;
using TMPro;
using UnityEngine;
using Zenject;

namespace Randomizer.ExternalFrameworks.Views
{
    public class ItemView : ZenjectProducableObject, IItemView
    {
        [SerializeField] private TMP_Text text;
        
        [SerializeField] private bool handleEmpty;
        [SerializeField] private string emptyText = "(Untitled)";
        [SerializeField] private Color emptyColor = Color.gray;
        
        private Color activeColor;

        private void Awake()
        {
            activeColor = text.color;
        }

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
            set => UpdateText(value);
        }

        private void ArrangeByOrder(int order)
        {
            transform.SetSiblingIndex(order);
        }

        private void UpdateText(string value)
        {
            if (!handleEmpty)
            {
                text.text = value;
                return;
            }
            
            var isEmpty = string.IsNullOrEmpty(value);
            text.text = isEmpty ? emptyText : value;
            text.color = isEmpty ? emptyColor : activeColor;
        }
        
        public class Factory : PlaceholderFactory<ItemView>
        {
        }
    }
}