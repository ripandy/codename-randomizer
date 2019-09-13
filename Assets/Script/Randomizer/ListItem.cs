using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListItem : MonoBehaviour
{
    [SerializeField] TMP_Text _itemLabel;
    public string ItemText { get; private set; }

    public void SetItemLabel(string itemText)
    {
        ItemText = itemText;
        _itemLabel.text = ItemText;
    }
}
