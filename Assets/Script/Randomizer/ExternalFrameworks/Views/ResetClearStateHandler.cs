using Randomizer.ExternalFrameworks.Views;
using TMPro;
using UniRx;
using UnityEngine;

public class ResetClearStateHandler : MonoBehaviour
{
    [SerializeField] private BaseView resetButton;
    [SerializeField] private BaseView clearButton;
    [SerializeField] private GameObject buttonBase;
    [SerializeField] private TMP_Text caption;
    
    private void Start()
    {
        BindReactive();
    }

    private void BindReactive()
    {
        this.ObserveEveryValueChanged(_ => resetButton.Visible)
            .Subscribe(_ => UpdateState(_, clearButton.Visible))
            .AddTo(this);
        
        this.ObserveEveryValueChanged(_ => clearButton.Visible)
            .Subscribe(_ => UpdateState(resetButton.Visible, _))
            .AddTo(this);
    }

    private void UpdateState(bool resetButtonState, bool clearButtonState)
    {
        buttonBase.SetActive(resetButtonState || clearButtonState);
        if (resetButtonState)
            caption.text = "Reset";
        if (clearButtonState)
            caption.text = "Clear";
    }
}
