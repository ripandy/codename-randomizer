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

        private const float AnimationDuration = 0.2f;
        
        private EventSystem _eventSystem;
        private ButtonState _state;
        private bool _animate;
        
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
            inputField.onSubmit.AddListener(s => SetState(ButtonState.Inactive));
            inputField.onDeselect.AddListener((s) => SetState(ButtonState.Inactive));
            this.ObserveEveryValueChanged(_ => inputFieldView.Visible)
                .Subscribe(gameObject.SetActive)
                .AddTo(this);
        }

        private void OnClick()
        {
            var newState = (int) _state + 1;
            var len = Enum.GetValues(typeof(ButtonState)).Length;
            SetState((ButtonState) (newState % len));
        }
        
        private void SetState(ButtonState newState)
        {
            if (_state == newState || _animate) return;
            AnimateButton(newState == ButtonState.Active);
            ShowInputField(newState == ButtonState.Active);
            _animate = true;
            _state = newState;
        }

        private void AnimateButton(bool active)
        {
            // var targetRotation = active ? -45 : 0;
            var targetRotation = captionTransform.rotation.eulerAngles.z + 45;
            var ease = active ? Ease.InQuad : Ease.OutQuad;
            captionTransform.DORotate(new Vector3(0f, 0f, targetRotation), AnimationDuration)
                .SetEase(ease)
                .OnComplete(AnimationDone);
        }

        private void ShowInputField(bool show)
        {
            var targetY = show ? 0 : -inputFieldContainer.rect.height;
            var ease = show ? Ease.InQuad : Ease.OutQuad;
            var targetPos = inputFieldContainer.anchoredPosition;
            targetPos.y = targetY;
            
            DOTween.To(() => inputFieldContainer.anchoredPosition,
                    value => inputFieldContainer.anchoredPosition = value, targetPos, AnimationDuration)
                .SetEase(ease)
                .OnComplete(() => SelectInputField(show));
        }

        private void SelectInputField(bool select)
        {
            if (select)
                inputField.Select();
            else
                _eventSystem.SetSelectedGameObject(null);
        }

        private void AnimationDone()
        {
            _animate = false;
        }
    }
}