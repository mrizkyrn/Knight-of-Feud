using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject characterSheetMenu;
    [SerializeField] private PlayerStatsUI playerStatsUI;

    private void Start()
    {
        characterSheetMenu.SetActive(false);
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
            playerStatsUI.SetStats();
        }
        else
        {
            Time.timeScale = 1f;
        }

        characterSheetMenu.SetActive(!characterSheetMenu.activeSelf);
    }
}
