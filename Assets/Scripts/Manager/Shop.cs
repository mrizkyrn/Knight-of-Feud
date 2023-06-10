using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject shopMenu;

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
    }

    public void Close()
    {
        shopMenu.SetActive(false);
    }
}
