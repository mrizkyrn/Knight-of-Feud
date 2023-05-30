using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; private set; }

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemContent;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private Button useButton;
    [SerializeField] private Button dropButton;

    private List<Item> items = new List<Item>();

    public event Action<ItemSlotController> OnItemSelected;
    public event Action<ItemSlotController> OnItemDeselected;

    public ItemSlotController selectedItem;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        itemName.text = "";
        itemDescription.text = "";
    }

    private void Start()
    {
        useButton.onClick.AddListener(UseItem);
        dropButton.onClick.AddListener(DropItem);
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void ItemList()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in items)
        {
            GameObject obj = Instantiate(itemPrefab, itemContent);

            var icon = obj.transform.Find("Image").GetComponent<Image>();
            var itemController = obj.GetComponent<ItemSlotController>();

            icon.sprite = item.icon;
            itemController.item = item;
        }
    }

    public void SetData(string name, string description)
    {
        itemName.text = name;
        itemDescription.text = description;
    }

    public void SelectItem(ItemSlotController itemSlot)
    {
        if (selectedItem == itemSlot)
        {
            OnItemDeselected?.Invoke(itemSlot);
            selectedItem = null;
        }
        else
        {
            selectedItem = itemSlot;

            OnItemSelected?.Invoke(itemSlot);
        }
    }

    public void UseItem()
    {
        if (selectedItem != null)
        {
            Item item = selectedItem.item;
            Debug.Log(PlayerStats.Instance.Health.CurrentValue);
            PlayerStats.Instance.Health.Increase(10f);
            Debug.Log(PlayerStats.Instance.Health.CurrentValue);
            items.Remove(item);
            ItemList();
        }
    }

    public void DropItem()
    {
        if (selectedItem != null)
        {
            Item item = selectedItem.item;
            items.Remove(item);
            ItemList();
        }
    }
}