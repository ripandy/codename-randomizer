using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class RandomizerScene : MonoBehaviour
{
    const float DISTANCE_BETWEEN_ITEMS = 140f;

    // Unity objects fieldn;
    [SerializeField] RectTransform contentBase;
    [SerializeField] Button randomizeButton;
    [SerializeField] RectTransform addItemButton;
    [SerializeField] TMP_InputField addItemField;
    [SerializeField] GameObject itemPrefab;

    // logic field
    List<ListItem> items;
    Vector2 itemPositionOffset;

    // cache
    GameManager m_GameManager;

    void Start()
    {
        m_GameManager = GameManager.Instance;

        items = new List<ListItem>();

        addItemField.onSubmit.AddListener(AddItem);
        addItemField.onSelect.AddListener(_ => {
            Debug.Log("selected!");
            addItemField.placeholder.gameObject.SetActive(false);
        });

        randomizeButton.onClick.AddListener(Randomize);

        itemPositionOffset = addItemButton.anchoredPosition;
    }

    void AddItem(string itemName)
    {
        if (!string.IsNullOrEmpty(itemName)) {
            var obj = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, contentBase);
            obj.GetComponent<RectTransform>().anchoredPosition = itemPositionOffset;
            var item = obj.GetComponent<ListItem>();
                item.SetItemLabel(itemName);

            items.Add(item);

            itemPositionOffset.y -= 120f;
            addItemButton.anchoredPosition = itemPositionOffset;

            var sizeDelta = itemPositionOffset;
                sizeDelta.y *= -1;
                sizeDelta.y += 340;
            contentBase.sizeDelta = sizeDelta;
        }

        addItemField.placeholder.gameObject.SetActive(true);
        addItemField.text = "";
        
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Randomize()
    {
        if (items.Count <= 0) return;
        
        var rndNum = Random.Range(0, items.Count);
        string rndVal = items[rndNum].ItemText;
        Debug.Log("Random Result : " + rndVal);
    }
}
