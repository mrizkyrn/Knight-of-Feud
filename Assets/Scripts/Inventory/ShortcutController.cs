using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortcutController : MonoBehaviour
{
    [SerializeField] private GameObject shortcutSlotPrefab;
    [SerializeField] private int amount;

    // private List<Item> items = new List<Item>();
    private List<ShortcutSlot> shortcutSlots = new List<ShortcutSlot>();

    private void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject GO = Instantiate(shortcutSlotPrefab, transform);

            ShortcutSlot shortcutSlot = GO.GetComponent<ShortcutSlot>();
            shortcutSlot.OnDeleteButtonPressed += RemoveItemFromShortcut;
            shortcutSlots.Add(shortcutSlot);
        }
    }

    public void AddToShortcut(Item item)
    {
        foreach (var slot in shortcutSlots)
        {
            if (slot.item == null)
            {
                slot.item = item;
                ShortcutUpdate();
                break;
            }
        }
    }

    private void ShortcutUpdate()
    {
        foreach (var slot in shortcutSlots)
        {
            if (slot.item != null)
            {
                slot.itemImage.sprite = slot.item.icon;
                slot.SetImageActive(true);
            }
        }
    }

    private void RemoveItemFromShortcut(ShortcutSlot shortcutSlot)
    {
        shortcutSlot.item = null;
        shortcutSlot.SetImageActive(false);
        ShortcutUpdate();
    }
}
