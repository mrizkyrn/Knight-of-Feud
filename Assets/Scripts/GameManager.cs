using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject characterSheetMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button noButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private PlayerStatsUI playerStatsUI;

    private void Start()
    {
        characterSheetMenu.SetActive(false);
        pauseMenu.SetActive(false);

        noButton.onClick.AddListener(Resume);
        yesButton.onClick.AddListener(BactToMainMenu);
    }

    private void OnEnable()
    {
        InputHandler.Instance.OnInventoryPressed += OpenCharacterMenu;
        InputHandler.Instance.OnPausePressed += OpenPauseMenu;
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnInventoryPressed -= OpenCharacterMenu;
        InputHandler.Instance.OnPausePressed -= OpenPauseMenu;
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

    private void OpenPauseMenu()
    {
        if (!pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    private void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private void BactToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
