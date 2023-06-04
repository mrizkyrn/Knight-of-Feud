using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortcutSlot : MonoBehaviour
{
    public event Action<ShortcutSlot> OnDeleteButtonPressed;

    [SerializeField] private Button deleteButton;
    [SerializeField] public Image itemImage;

    public Item item;

    private void Start()
    {
        deleteButton.onClick.AddListener(DeleteItemFromShortcut);
        SetImageActive(false);
    }

    public void SetImageActive(bool isActive)
    {
        itemImage.gameObject.SetActive(isActive);
    }

    private void DeleteItemFromShortcut()
    {
        if (item != null)
        {
            OnDeleteButtonPressed?.Invoke(this);
            
            Debug.Log("deleted");
        }

    }
}
