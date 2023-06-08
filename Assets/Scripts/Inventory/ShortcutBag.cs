using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShortcutBag : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemAmount;

    public Item item;

    private void Start()
    {
        SetImageActive(false);
    }

    public void SetItemImage(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }

    public void SetAmount(int amount)
    {
        itemAmount.SetText(amount.ToString());
    }

    public void SetImageActive(bool isActive)
    {
        itemImage.gameObject.SetActive(isActive);
    }
}
