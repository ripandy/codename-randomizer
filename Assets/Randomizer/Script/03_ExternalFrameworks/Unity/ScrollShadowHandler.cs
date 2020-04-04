using UnityEngine;
using UnityEngine.UI;

public class ScrollShadowHandler : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Shadow shadow;

    private Color _defaultColor;

    private void Start()
    {
        _defaultColor = shadow.effectColor;
        scrollRect.onValueChanged.AddListener(UpdateShadow);
    }

    private void UpdateShadow(Vector2 distance)
    {
        var c = _defaultColor;
            c.a = Mathf.Clamp01((1 - distance.y) * 10);
        shadow.effectColor = c;
    }
}
