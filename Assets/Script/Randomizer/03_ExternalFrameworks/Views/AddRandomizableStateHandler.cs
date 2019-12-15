using System;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Randomizer.ExternalFrameworks.Views
{
    public class AddRandomizableStateHandler : MonoBehaviour
    {
        private enum ButtonState
        {
            Inactive,
            Active
        }

        [SerializeField] private BaseView inputFieldView;
        [SerializeField] private RectTransform inputFieldContainer;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button addButton;
        [SerializeField] private RectTransform captionTransform;

        private const float AnimationDuration = 0.6f;
        
        private EventSystem _eventSystem;
        private ButtonState _state;
        
        private void Awake()
        {
            _eventSystem = EventSystem.current;
        }

        private void Start()
        {
            BindReactive();
        }

        private void BindReactive()
        {
            addButton.onClick.AddListener(OnClick);
            this.ObserveEveryValueChanged(_ => inputFieldView.Visible)
                .Subscribe(gameObject.SetActive)
                .AddTo(this);
        }

        private void OnClick()
        {
            var newState = (int) ++_state;
            var len = Enum.GetValues(typeof(ButtonState)).Length;
            _state = (ButtonState) (newState % len);
            
            // animate input field
            var targetY = _state == ButtonState.Active ? 0 : -inputFieldContainer.rect.height;
            var ease = _state == ButtonState.Active ? Ease.InQuad : Ease.OutQuad;
            inputFieldContainer.DOMoveY(targetY, AnimationDuration)
                .SetEase(ease)
                .OnComplete(() => SelectInputField(_state == ButtonState.Active));
            
            // animate button
            var targetRotation = _state == ButtonState.Inactive ? 0 : 45;
            captionTransform.DORotate(new Vector3(0f, 0f, targetRotation), AnimationDuration)
                .SetEase(ease);
        }

        private void SelectInputField(bool select)
        {
            if (select)
                inputField.Select();
            else
                _eventSystem.SetSelectedGameObject(null);
        }
    }
}