using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; private set; }

    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Transform itemContent;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private Button useButton;
    [SerializeField] private Button dropButton;
    [SerializeField] private int maxInventorySlot;

    private List<Item> items = new List<Item>();
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
    }

    private void Start()
    {
        useButton.onClick.AddListener(UseItem);
        dropButton.onClick.AddListener(DropItem);

        SetData("", "");
        InitSlots();
    }

  private void InitSlots()
    {
        for (int i = 0; i < maxInventorySlot; i++)
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

            if (i < items.Count)
            {
                icon.sprite = items[i].icon;
                slots[i].item = items[i];
                icon.gameObject.SetActive(true);
            }
            else
            {
                icon.sprite = null;
                slots[i].item = null;
            }
        }
    }

    public void ResetData()
    {
        foreach (ItemSlot slot in slots)
            slot.Reset();
        SetData("", "");
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

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
            Debug.Log(PlayerStats.Instance.Health.CurrentValue);
            selectedItem.Use();
            Debug.Log(PlayerStats.Instance.Health.CurrentValue);
            items.Remove(selectedItem.item);
            UpdateSlots();
        }
    }

    public void DropItem()
    {
        if (selectedItem.item != null)
        {
            items.Remove(selectedItem.item);
            UpdateSlots();
        }
    }
}