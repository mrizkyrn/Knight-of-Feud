using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject itemShopSlotPrefab;
    [SerializeField] private Transform potionsContent;
    [SerializeField] private Item[] potionItems;
    [SerializeField] private Transform weaponsContent;
    [SerializeField] private Item[] WeaponItems;
    // [SerializeField] private Transform sellContent;

    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text nameTxt;
    [SerializeField] private TMP_Text descTxt;
    [SerializeField] private TMP_Text priceTxt;

    [SerializeField] private Button buyButton;
    [SerializeField] private AudioSource buySFX;

    public event Action<ItemShopSlot> OnItemSelected;
    public event Action<ItemShopSlot> OnItemDeselected;

    public ItemShopSlot selectedItem;

    private void Start()
    {
        weaponsContent.gameObject.SetActive(false);

        for (int i = 0; i < potionItems.Length; i++)
        {
            GameObject slotGO = Instantiate(itemShopSlotPrefab, potionsContent);
            ItemShopSlot itemShopSlot = slotGO.GetComponent<ItemShopSlot>();
            itemShopSlot.item = potionItems[i];

            var icon = slotGO.transform.Find("Image").GetComponent<Image>();
            icon.sprite = potionItems[i].icon;
        }

        for (int i = 0; i < WeaponItems.Length; i++)
        {
            GameObject slotGO = Instantiate(itemShopSlotPrefab, weaponsContent);
            ItemShopSlot itemShopSlot = slotGO.GetComponent<ItemShopSlot>();
            itemShopSlot.item = WeaponItems[i];

            var icon = slotGO.transform.Find("Image").GetComponent<Image>();
            icon.sprite = WeaponItems[i].icon;
        }

        buyButton.onClick.AddListener(BuyItem);
        itemImage.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
    }

    public void SetDescription()
    {
        itemImage.sprite = selectedItem.item.icon;
        nameTxt.text = selectedItem.item.name;
        descTxt.text = selectedItem.item.description;
        priceTxt.text = selectedItem.item.buyingPrice.ToString();
    }

    public void ResetDescription()
    {
        itemImage.sprite = null;
        nameTxt.text = "";
        descTxt.text = "";
    }

    public void SelectItem(ItemShopSlot itemShopSlot)
    {
        if (selectedItem == itemShopSlot)
        {
            OnItemDeselected?.Invoke(itemShopSlot);
            itemImage.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
            selectedItem = null;
        }
        else
        {
            selectedItem = itemShopSlot;
            itemImage.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(true);
            OnItemSelected?.Invoke(itemShopSlot);
        }
    }

    private void BuyItem()
    {
        if (selectedItem != null)
        {
            if (PlayerStats.Instance.Gold.CurrentValue >= selectedItem.item.buyingPrice)
            {
                buySFX.Play();
                InventoryManager.Instance.AddItem(selectedItem.item);
                PlayerStats.Instance.Gold.Decrease(selectedItem.item.buyingPrice);
                Debug.Log("Item berhasil dibeli");
            }
            else
            {
                Debug.Log("Not enough gold");
            }
        }
        else
        {
            Debug.Log("Item tidak tersedia");
        }
    }
}
