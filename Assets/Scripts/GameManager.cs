using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject characterSheetMenu;

    private void Start()
    {
        characterSheetMenu.SetActive(false);
    }

    // private void Update()
    // {
    //     if (InputHandler.Instance != null)
    //     {
    //         InventoryController.Instance.ItemList();
    //         characterSheetMenu.SetActive(InputHandler.Instance.isInventoryOpen);
    //     }
    // }

    private void OnEnable()
    {
        InputHandler.Instance.OnInventoryPressed += OpenCharacterMenu;
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnInventoryPressed -= OpenCharacterMenu;
    }

    private void OpenCharacterMenu()
    {
        InventoryController.Instance.UpdateSlots();
        characterSheetMenu.SetActive(!characterSheetMenu.activeSelf);
    }
}
