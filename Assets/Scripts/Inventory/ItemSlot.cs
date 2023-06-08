using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject borderImage;
    [SerializeField] private GameObject image;

    public Item item;

    public void Awake()
    {
        borderImage.SetActive(false);
        image.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryMenu.Instance.OnItemSelected += OnItemSelected;
        InventoryMenu.Instance.OnItemDeselected += OnItemDeselected;
    }

    private void OnDisable()
    {
        InventoryMenu.Instance.OnItemSelected -= OnItemSelected;
        InventoryMenu.Instance.OnItemDeselected -= OnItemDeselected;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryMenu.Instance.SelectItem(this);
    }

    private void OnItemSelected(ItemSlot selectedItem)
    {
        if (selectedItem == this)
        {
            borderImage.SetActive(true);
            if (item != null)
                InventoryMenu.Instance.SetData(item.itemName, item.description);
            else
                InventoryMenu.Instance.SetData("", "");
        }
        else
        {
            borderImage.SetActive(false);
        }
    }

    private void OnItemDeselected(ItemSlot deselectedItem)
    {
        if (deselectedItem == this)
        {
            // The item is deselected, perform deselection visual changes
            InventoryMenu.Instance.SetData("", "");
            borderImage.SetActive(false);
        }
    }

    public void Use()
    {
        if (item.itemType == Item.ItemType.Potion)
        {
            if (item.affect == Item.Affect.Health)
            {
                switch (item.level)
                {
                    case 1: PlayerStats.Instance.Health.Increase(PlayerStats.Instance.Health.MaxValue * 0.25f); break;
                    case 2: PlayerStats.Instance.Health.Increase(PlayerStats.Instance.Health.MaxValue * 0.50f); break;
                    case 3: PlayerStats.Instance.Health.Increase(PlayerStats.Instance.Health.MaxValue * 1f); break;
                    default: Debug.LogError("level item not assigned"); break;
                }
            }
        }
    }

    public void Reset()
    {
        borderImage.SetActive(false);
        image.SetActive(false);
    }
}
