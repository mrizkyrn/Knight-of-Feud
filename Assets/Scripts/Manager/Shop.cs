using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private ShopManager shopManager;

    private void Start()
    {
        UI.SetActive(false);
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
        shopManager.SetActiveShopMenu(true);
        shopManager.SetSellContent();
        Time.timeScale = 0f;
    }

    public void Close()
    {
        shopManager.SetActiveShopMenu(false);
        shopManager.ResetSellContent();
        Time.timeScale = 1f;
    }
}
