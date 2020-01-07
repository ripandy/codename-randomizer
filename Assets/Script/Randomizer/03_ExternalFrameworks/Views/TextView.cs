using Randomizer.InterfaceAdapters;
using TMPro;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.Views
{
    public class TextView : BaseView, ITextView
    {
        [SerializeField] private TMP_Text text;
        public string Text
        {
            get => text.text;
            set => text.text = value;
        }
    }
}