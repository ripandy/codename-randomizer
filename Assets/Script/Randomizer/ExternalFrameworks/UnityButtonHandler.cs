using System;
using System.Collections.Generic;
using Randomizer.InterfaceAdapters;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Randomizer.ExternalFrameworks
{
    public class UnityButtonHandler : MonoBehaviour, IButtonHandler
    {
        [SerializeField] private Button button;

        private readonly IList<Action> _subscribers = new List<Action>();
        private IObservable<Unit> _buttonObservable;

        private void Start()
        {
            BindReactive();
        }

        private void BindReactive()
        {
            _buttonObservable = button.OnClickAsObservable();
            foreach (var onPress in _subscribers)
            {
                _buttonObservable.Subscribe(_ => onPress.Invoke()).AddTo(this);
            }
        }

        public void Subscribe(Action onPress)
        {
            if (_buttonObservable != null)
            {
                _buttonObservable.Subscribe(_ => onPress.Invoke()).AddTo(this);
                Debug.Log("Subscription happened after initialization!!");
            }
            _subscribers.Add(onPress);
        }
    }
}