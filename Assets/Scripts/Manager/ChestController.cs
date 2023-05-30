using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private int minGold;
    [SerializeField] private int maxGold;
    [SerializeField] private List<Item> items;

    private bool isOpened;
    private bool isOnChestArea;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isOpened = false;
    }

    private void Start()
    {
        InputHandler.Instance.OnOpenChestPressed += OpenChest;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOnChestArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOnChestArea = false;
        }
    }

    private void AddRandomGold()
    {
        int goldToAdd = Random.Range(minGold, maxGold + 1);

        PlayerStats.Instance.Gold.Increase(goldToAdd);
        Debug.Log("+ " + goldToAdd + " gold.");
    }

    private void AddRandomItem()
    {
        // Generate a random number between 0 and the total chance
        float randomNumber = Random.Range(0f, 1f);

        // Iterate through the items and check their chance to find the randomly selected item
        foreach (Item item in items)
        {
            randomNumber = Random.Range(0f, 1f);

            // If the random number is within the current chance range, add the item to the inventory
            if (randomNumber <= item.chance)
            {
                InventoryController.Instance.Add(item);
                Debug.Log("+1 " + item.itemName);
            }
        }
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnOpenChestPressed -= OpenChest;
    }

    private void OpenChest()
    {
        if (isOnChestArea && !isOpened)
        {
            animator.SetTrigger("Open");
        }
    }

    public void Open()
    {
        AddRandomGold();
        AddRandomItem();

        isOpened = true;
    }
}
