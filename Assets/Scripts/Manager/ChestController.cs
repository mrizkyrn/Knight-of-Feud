using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemChance
{
    public Item item;
    [Range(0f, 1f)]
    public float chance;
}

public class ChestController : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private int minGold;
    [SerializeField] private int maxGold;
    [SerializeField] private GameObject openTextUI;
    [SerializeField] private Sprite coinImage;
    [SerializeField] private List<ItemChance> itemChances;
    [SerializeField] private CollectedItem UI;

    private AudioSource collectSound;

    private bool isOpened;
    private bool isOnChestArea;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isOpened = false;

        collectSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        InputHandler.Instance.OnOpenChestPressed += OpenChest;
        openTextUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            isOnChestArea = true;
            openTextUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOnChestArea = false;
            openTextUI.SetActive(false);
        }
    }

    private void AddRandomGold()
    {
        int goldToAdd = Random.Range(minGold, maxGold + 1);

        PlayerStats.Instance.Gold.Increase(goldToAdd);
        Debug.Log("+ " + goldToAdd + " gold.");

        UI.SetCollectItemUI(coinImage, "Gold", goldToAdd.ToString());
    }

    private void AddRandomItem()
    {
        // Generate a random number between 0 and the total chance
        float randomNumber = Random.Range(0f, 1f);

        // Iterate through the items and check their chance to find the randomly selected item
        foreach (ItemChance itemChance in itemChances)
        {
            randomNumber = Random.Range(0f, 1f);

            // If the random number is within the current chance range, add the item to the inventory
            if (randomNumber <= itemChance.chance)
            {
                InventoryManager.Instance.AddItem(itemChance.item);

                UI.SetCollectItemUI(itemChance.item.icon, itemChance.item.itemName, "1");
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
        collectSound.Play();
        AddRandomGold();
        AddRandomItem();

        isOpened = true;
    }
}
