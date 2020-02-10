using Randomizer.InterfaceAdapters;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Randomizer.ExternalFrameworks.Views
{
    public class PickLabelView : ItemView, IPickLabelView
    {
        [SerializeField] private Image toggleImage;
        [SerializeField] private Sprite checkedSprite;
        [SerializeField] private Sprite uncheckedSprite;

        private void Awake()
        {
            if (checkedSprite == null)
                checkedSprite = toggleImage.sprite;
        }

        private bool _toggle;
        public bool Toggle
        {
            get => _toggle;
            set => SetToggle(value);
        }

        private void SetToggle(bool toggle)
        {
            _toggle = toggle;
            toggleImage.sprite = _toggle ? checkedSprite : uncheckedSprite;
        }
        
        public new class Factory : PlaceholderFactory<PickLabelView>
        {
        }
    }
}