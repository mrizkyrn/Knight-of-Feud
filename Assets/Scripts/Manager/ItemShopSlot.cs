using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemShopSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject borderImage;
    [SerializeField] private GameObject image;
    
    private AudioSource selectSound;
    private ShopManager shopManager;

    public Item item;

    public void Awake()
    {
        borderImage.SetActive(false);
        shopManager = GameObject.Find("GameManager").GetComponent<ShopManager>();
        selectSound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        shopManager.OnItemSelected += OnItemSelected;
        shopManager.OnItemDeselected += OnItemDeselected;
    }

    private void OnDisable()
    {
        shopManager.OnItemSelected -= OnItemSelected;
        shopManager.OnItemDeselected -= OnItemDeselected;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerClick.gameObject.name);
        shopManager.SelectItem(this);
    }

    private void OnItemSelected(ItemShopSlot selectedItem)
    {
        selectSound.Play();
        if (selectedItem == this)
        {
            borderImage.SetActive(true);
            shopManager.SetDescription();
        }
        else
        {
            borderImage.SetActive(false);
        }
    }

    private void OnItemDeselected(ItemShopSlot deselectedItem)
    {
        if (deselectedItem == this)
        {
            shopManager.ResetDescription();
            borderImage.SetActive(false);
        }
    }

    public void Reset()
    {
        borderImage.SetActive(false);
        image.SetActive(false);
    }
}
