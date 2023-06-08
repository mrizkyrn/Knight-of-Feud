using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortcutController : MonoBehaviour
{
    [SerializeField] private GameObject shortcutSlotPrefab;
    
    public void Start()
    {
        for (int i = 0; i < InventoryManager.Instance.shortcutAmount; i++)
        {
            GameObject slotGO = Instantiate(shortcutSlotPrefab, transform);
            ShortcutSlot shortcutSlot = slotGO.GetComponent<ShortcutSlot>();
            InventoryManager.Instance.itemShortcutSlots[i] = shortcutSlot;

            shortcutSlot.OnDeleteButtonPressed += InventoryManager.Instance.RemoveItemFromShortcut;
        }
    }

    // public void AddToShortcut(Item item)
    // {
    //     for (int i = 0; i < InventoryManager.Instance.itemShortcutSlots.Length; i++)
    //     {
    //         if (InventoryManager.Instance.itemShortcutSlots[i].item == null)
    //         {
    //             InventoryManager.Instance.itemShortcutSlots[i].item = item;
    //             ShortcutUpdate();
    //             break;
    //         }
    //     }

    //     shortcutBags.AddToBag(item);
    // }



    // private void ShortcutUpdate()
    // {
    //     for (int i = 0; i < InventoryManager.Instance.itemShortcutSlots.Length; i++)
    //     {
    //         if (InventoryManager.Instance.GetItemAmount(InventoryManager.Instance.itemShortcutSlots[i].item) < 1)
    //         {
    //             InventoryManager.Instance.itemShortcutSlots[i].item = null;
    //             InventoryManager.Instance.itemShortcutSlots[i].itemImage.sprite = null;
    //             InventoryManager.Instance.itemShortcutSlots[i].SetImageActive(false);
    //             shortcutBags.RemoveItemFromBag(i);
    //         }
    //         else if (InventoryManager.Instance.itemShortcutSlots[i].item != null)
    //         {
    //             InventoryManager.Instance.itemShortcutSlots[i].itemImage.sprite = InventoryManager.Instance.itemShortcutSlots[i].item.icon;
    //             InventoryManager.Instance.itemShortcutSlots[i].SetImageActive(true);
    //         }
    //     }
    // }

    // private void RemoveItemFromShortcut(ShortcutSlot shortcutSlot)
    // {
    //     for (int i = 0; i < InventoryManager.Instance.itemShortcutSlots.Length; i++)
    //     {
    //         if (InventoryManager.Instance.itemShortcutSlots[i] == shortcutSlot)
    //         {
    //             InventoryManager.Instance.itemShortcutSlots[i].item = null;
    //             InventoryManager.Instance.itemShortcutSlots[i].SetImageActive(false);
    //             ShortcutUpdate();

    //             shortcutBags.RemoveItemFromBag(i);
    //             break;
    //         }
    //     }
    // }



 
}
