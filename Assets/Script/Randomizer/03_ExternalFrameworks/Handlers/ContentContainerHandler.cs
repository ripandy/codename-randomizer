using UnityEngine;

public class ContentContainerHandler : MonoBehaviour
{
    public RectTransform Container => transform as RectTransform;
    [SerializeField] private RectTransform top;
    [SerializeField] private RectTransform bottom;

    public void FinalizeOrder()
    {
        if (top != null) top.SetAsFirstSibling();
        if (bottom != null) bottom.SetAsLastSibling();
    }
}
