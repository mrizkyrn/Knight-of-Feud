using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject characterSheetMenu;
    [SerializeField] private GameObject shopMenu;

    private void Start()
    {
        characterSheetMenu.SetActive(false);
    }

    private void Update()
    {
        if (!shopMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

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
        if (!characterSheetMenu.activeSelf)
        {
            Time.timeScale = 0f;
            InventoryMenu.Instance.UpdateSlots();
        }
        else
        {
            Time.timeScale = 1f;
        }

        characterSheetMenu.SetActive(!characterSheetMenu.activeSelf);
    }
}
