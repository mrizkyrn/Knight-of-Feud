using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance { get; private set; }

    [SerializeField] private ShortcutBags shortcutBags;

    public int maxInventorySlot;
    public int shortcutAmount;

    public List<Item> inventoryItems = new List<Item>();
    public ShortcutSlot[] itemShortcutSlots;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        itemShortcutSlots = new ShortcutSlot[shortcutAmount];
    }

    public void AddItem(Item item)
    {
        inventoryItems.Add(item);
        // ShortcutUpdate();
    }

    public void RemoveItem(Item item)
    {
        inventoryItems.Remove(item);
        ShortcutUpdate();
    }

    public void AddToShortcut(Item item)
    {
        for (int i = 0; i < itemShortcutSlots.Length; i++)
        {
            if (itemShortcutSlots[i].item == null)
            {
                itemShortcutSlots[i].item = item;
                ShortcutUpdate();
                break;
            }
        }

        shortcutBags.AddToBag(item);
    }

    public void ShortcutUpdate()
    {
        for (int i = 0; i < itemShortcutSlots.Length; i++)
        {
            if (GetItemAmount(itemShortcutSlots[i].item) < 1)
            {
                itemShortcutSlots[i].item = null;
                itemShortcutSlots[i].itemImage.sprite = null;
                itemShortcutSlots[i].SetImageActive(false);
                shortcutBags.RemoveItemFromBag(i);
            }
            else if (itemShortcutSlots[i].item != null)
            {
                itemShortcutSlots[i].itemImage.sprite = itemShortcutSlots[i].item.icon;
                itemShortcutSlots[i].SetImageActive(true);
            }
        }

        shortcutBags.BagUpdate();
    }

    public void RemoveItemFromShortcut(ShortcutSlot shortcutSlot)
    {
        for (int i = 0; i < itemShortcutSlots.Length; i++)
        {
            if (itemShortcutSlots[i] == shortcutSlot)
            {
                itemShortcutSlots[i].item = null;
                itemShortcutSlots[i].SetImageActive(false);
                ShortcutUpdate();

                shortcutBags.RemoveItemFromBag(i);
                break;
            }
        }
    }

    public int GetItemAmount(Item itemtype)
    {
        int amount = 0;
        foreach (var item in inventoryItems)
        {
            if (item == itemtype)
            {
                amount++;
            }
        }
        
        return amount;
    }
}
