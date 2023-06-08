using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutBags : MonoBehaviour
{
    private ShortcutBag[] itemShortcutBags;
    
    [SerializeField] private GameObject shortcutBagPrefab;
    
    public void Start()
    {
        InputHandler.Instance.OnUseItem1Pressed += UseItem1;
        InputHandler.Instance.OnUseItem2Pressed += UseItem2;
        InputHandler.Instance.OnUseItem3Pressed += UseItem3;

        itemShortcutBags = new ShortcutBag[InventoryManager.Instance.shortcutAmount];

        for (int i = 0; i < InventoryManager.Instance.shortcutAmount; i++)
        {
            GameObject bagGO = Instantiate(shortcutBagPrefab, transform);
            ShortcutBag shortcutBag = bagGO.GetComponent<ShortcutBag>();
            itemShortcutBags[i] = shortcutBag;
        }
    }

    public void AddToBag(Item item)
    {
        for (int i = 0; i < itemShortcutBags.Length; i++)
        {
            if (itemShortcutBags[i].item == null)
            {
                itemShortcutBags[i].item = item;
                BagUpdate();
                break;
            }
        }
    }

    public void RemoveItemFromBag(int index)
    {
        itemShortcutBags[index].item = null;
        itemShortcutBags[index].SetImageActive(false);
        itemShortcutBags[index].SetAmount(0);
        BagUpdate();
    }

    public void BagUpdate()
    {
        for (int i = 0; i < itemShortcutBags.Length; i++)
        {
            if (itemShortcutBags[i].item != null)
            {
                itemShortcutBags[i].SetItemImage(itemShortcutBags[i].item.icon);
                itemShortcutBags[i].SetImageActive(true);
                itemShortcutBags[i].SetAmount(InventoryManager.Instance.GetItemAmount(itemShortcutBags[i].item));
            }
        }
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnUseItem1Pressed -= UseItem1;
        InputHandler.Instance.OnUseItem2Pressed -= UseItem2;
        InputHandler.Instance.OnUseItem3Pressed -= UseItem3;
    }

    private void UseItem1()
    {
        if (itemShortcutBags[0].item != null)
        {
            Debug.Log("ITEM 1 USED");
            itemShortcutBags[0].item.PerformEffect();
            InventoryManager.Instance.RemoveItem(itemShortcutBags[0].item);
        }
    }
    private void UseItem2()
    {
        if (itemShortcutBags[1].item != null)
        {
            Debug.Log("ITEM 2 USED");
            itemShortcutBags[1].item.PerformEffect();
            InventoryManager.Instance.RemoveItem(itemShortcutBags[1].item);
        }
    }

    private void UseItem3()
    {
        if (itemShortcutBags[2].item != null)
        {
            Debug.Log("ITEM 3 USED");
            itemShortcutBags[2].item.PerformEffect();
            InventoryManager.Instance.RemoveItem(itemShortcutBags[2].item);
        }
    }

}
