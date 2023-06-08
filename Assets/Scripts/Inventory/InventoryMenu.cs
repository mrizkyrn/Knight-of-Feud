using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class InventoryMenu : MonoBehaviour
{
    public static InventoryMenu Instance { get; private set; }

    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Transform itemContent;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;

    [SerializeField] private Button useButton;
    [SerializeField] private Button dropButton;
    [SerializeField] private Button addShortcutButton;
    
    public ShortcutController shortcutSlots;

    // private List<Item> items = new List<Item>();
    private List<ItemSlot> slots = new List<ItemSlot>();

    public event Action<ItemSlot> OnItemSelected;
    public event Action<ItemSlot> OnItemDeselected;

    public ItemSlot selectedItem;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        InitSlots();
    }

    private void Start()
    {
        useButton.onClick.AddListener(UseItem);
        dropButton.onClick.AddListener(DropItem);
        addShortcutButton.onClick.AddListener(AddShorcut);

        SetData("", "");
        
    }

    private void InitSlots()
    {
        for (int i = 0; i < InventoryManager.Instance.maxInventorySlot; i++)
        {
            GameObject slotGO = Instantiate(itemSlotPrefab, itemContent);
            ItemSlot slot = slotGO.GetComponent<ItemSlot>();
            slots.Add(slot);
        }
    }

    public void UpdateSlots()
    {
        ResetData();

        for (int i = 0; i < slots.Count; i++)
        {
            var icon = slots[i].transform.Find("Image").GetComponent<Image>();

            if (i < InventoryManager.Instance.inventoryItems.Count)
            {
                icon.sprite = InventoryManager.Instance.inventoryItems[i].icon;
                slots[i].item = InventoryManager.Instance.inventoryItems[i];
                icon.gameObject.SetActive(true);
            }
            else
            {
                icon.sprite = null;
                slots[i].item = null;
            }
        }

        // InventoryManager.Instance?.ShortcutUpdate();
    }

    public void ResetData()
    {
        foreach (ItemSlot slot in slots)
            slot.Reset();
        SetData("", "");
    }

    // public void Add(Item item)
    // {
    //     items.Add(item);
    // }

    // public void Remove(Item item)
    // {
    //     items.Remove(item);
    // }

    public void SetData(string name, string description)
    {
        itemName.text = name;
        itemDescription.text = description;
    }

    public void SelectItem(ItemSlot itemSlot)
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
        if (selectedItem.item != null)
        {
            selectedItem.item.PerformEffect();
            InventoryManager.Instance.inventoryItems.Remove(selectedItem.item);
            UpdateSlots();
        }
    }

    public void DropItem()
    {
        if (selectedItem.item != null)
        {
            InventoryManager.Instance.inventoryItems.Remove(selectedItem.item);
            UpdateSlots();
        }
    }

    public void AddShorcut()
    {
        if (selectedItem.item != null && selectedItem.item.itemType == Item.ItemType.Potion)
        {
            InventoryManager.Instance.AddToShortcut(selectedItem.item);
        }
    }


}