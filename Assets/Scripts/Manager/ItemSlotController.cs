using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject borderImage;

    public Item item;

    public void Awake()
    {
        borderImage.SetActive(false);

    }

    private void OnEnable()
    {
        // Subscribe to the OnItemSelected and OnItemDeselected events
        InventoryController.Instance.OnItemSelected += OnItemSelected;
        InventoryController.Instance.OnItemDeselected += OnItemDeselected;
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnItemSelected and OnItemDeselected events
        InventoryController.Instance.OnItemSelected -= OnItemSelected;
        InventoryController.Instance.OnItemDeselected -= OnItemDeselected;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryController.Instance.SelectItem(this);
    }

    private void OnItemSelected(ItemSlotController selectedItem)
    {
        if (selectedItem == this)
        {
            InventoryController.Instance.SetData(item.itemName, item.description);
            borderImage.SetActive(true);
        }
        else
        {
            borderImage.SetActive(false);
        }
    }

    private void OnItemDeselected(ItemSlotController deselectedItem)
    {
        if (deselectedItem == this)
        {
            // The item is deselected, perform deselection visual changes
            InventoryController.Instance.SetData("", "");
            borderImage.SetActive(false);
        }
    }

    public void Use()
    {
        PlayerStats.Instance.Health.Increase(100f);
    }
}
