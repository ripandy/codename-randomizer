using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UniRx;

public class RandomizerScene : MonoBehaviour
{
    const float DISTANCE_BETWEEN_ITEMS = 140f;

    // Unity objects fieldn;
    [SerializeField] RectTransform contentBase;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject addItemObject;
    [SerializeField] TMP_InputField addItemField;
    [SerializeField] GameObject resultObject;
    [SerializeField] TMP_Text resultText;
    [SerializeField] Button randomizeButton;
    [SerializeField] GameObject resetButtonObject;
    [SerializeField] Button resetButton;
    [SerializeField] TMP_Text resetButtonText;

    // logic field
    List<ListItem> items;
    Vector2 defaultOffset;
    Vector2 itemPositionOffset;

    int state = 0;
    int itemCount = 0;

    void Start()
    {
        Initialize();
        BindReactive();

        Input.backButtonLeavesApp = true;
    }

    void BindReactive()
    {
        this.ObserveEveryValueChanged(_ => state)
            .Subscribe(value => OnStateChanged(value));

        this.ObserveEveryValueChanged(_ => itemCount)
            .Subscribe(value => resetButtonObject.SetActive(itemCount > 0));
    }

    void Initialize()
    {
        items = new List<ListItem>();

        addItemField.onSubmit.AddListener(AddItem);
        addItemField.onSelect.AddListener(_ => {
            addItemField.placeholder.gameObject.SetActive(false);
        });

        randomizeButton.onClick.AddListener(Randomize);
        resetButton.onClick.AddListener(OnResetButtonPressed);

        defaultOffset = addItemObject.GetComponent<RectTransform>().anchoredPosition;
        itemPositionOffset = defaultOffset;
    }

    void AddItem(string itemName)
    {
        if (!string.IsNullOrEmpty(itemName)) {
            ListItem item = null;
            if (items.Count <= itemCount)
            {
                var obj = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, contentBase);
                item = obj.GetComponent<ListItem>();
            }
            else
            {
                item = items[itemCount];
                item.gameObject.SetActive(true);
            }
            item.GetComponent<RectTransform>().anchoredPosition = itemPositionOffset;
            item.SetItemLabel(itemName);

            items.Add(item);

            itemPositionOffset.y -= 120f;
            addItemObject.GetComponent<RectTransform>().anchoredPosition = itemPositionOffset;

            var sizeDelta = Vector2.zero;
                sizeDelta.y = itemPositionOffset.y * -1;
                sizeDelta.y += 340;
            contentBase.sizeDelta = sizeDelta;

            itemCount++;
        }

        addItemField.placeholder.gameObject.SetActive(true);
        addItemField.text = "";
        
        EventSystem.current.SetSelectedGameObject(null);
    }

    void ResetItemList()
    {
        foreach (var item in items)
        {
            item.gameObject.SetActive(false);
        }
        itemPositionOffset = defaultOffset;

        addItemObject.GetComponent<RectTransform>().anchoredPosition = itemPositionOffset;

        var sizeDelta = Vector2.zero;
            sizeDelta.y = itemPositionOffset.y * -1;
            sizeDelta.y += 340;
        contentBase.sizeDelta = sizeDelta;

        itemCount = 0;
    }
    
    void SetState(int newState)
    {
        state = newState;
    }

    void OnStateChanged(int newState)
    {
        SetShowItemList(newState == 0);
        resetButtonText.text = (newState == 0 ? "Reset" : "Back");
    }

    void SetShowItemList(bool show)
    {
        addItemObject.SetActive(show);
        for (int i = 0; i < items.Count; i++)
        {
            items[i].gameObject.SetActive(show && i < itemCount);
        }
        resultObject.SetActive(!show);
    }

    void Randomize()
    {
        if (items.Count <= 0 || itemCount <= 0) return;
        
        var rndNum = Random.Range(0, itemCount);
        string rndVal = items[rndNum].ItemText;
        resultText.text = rndVal;

        if (state == 0)
            SetState(1);
    }

    void OnResetButtonPressed()
    {
        if (state == 1)
            SetState(0);
        else
            ResetItemList();
    }
}
