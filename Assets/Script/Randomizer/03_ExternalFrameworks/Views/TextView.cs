using Randomizer.InterfaceAdapters;
using TMPro;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.Views
{
    public class TextView : BaseView, ITextView
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private bool handleEmpty;
        [SerializeField] private string emptyText = "Title";
        [SerializeField] private Color emptyColor = Color.gray;
        private Color activeColor;

        private void Awake()
        {
            activeColor = text.color;
        }

        public string Text
        {
            get => text.text;
            set => SetText(value);
        }

        private void SetText(string value)
        {
            var isEmpty = string.IsNullOrEmpty(value) && handleEmpty;
            text.text = isEmpty ? emptyText : value;
            text.color = isEmpty ? emptyColor : activeColor;
        }
    }
}