using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private ShopManager shopManager;

    private void Start()
    {
        UI.SetActive(false);
        shopMenu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UI.SetActive(false);
        }
    }

    public void Open()
    {
        shopMenu.SetActive(true);
        shopManager.SetSellContent();
        Time.timeScale = 0f;
    }

    public void Close()
    {
        shopMenu.SetActive(false);
        shopManager.ResetSellContent();
        Time.timeScale = 1f;
    }
}
