using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    private Animator animator;

    [SerializeField] private enum ChestType
    {
        Common,
        Rare,
        Epic
    }

    private ChestType chestType;
    private int minGold;
    private int maxGold;
    private bool isOpened;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("OPEN CHest");
            if (!isOpened && Input.GetMouseButton(0))
            {
                animator.SetBool("Open", true);
            }
        }
    }

    public void Open()
    {
        int goldToAdd = GenerateRandomGoldAmount();
        playerStats.Gold.Increase(goldToAdd);
        Debug.Log("Added " + goldToAdd + " gold to player's inventory.");
        Debug.Log("Player gold now is " + playerStats.Gold.CurrentValue);

        isOpened = true;
    }

    private void SetChestType(ChestType type)
    {
        chestType = type;

        // Set the min and max gold values based on the chest type
        switch (chestType)
        {
            case ChestType.Common:
                minGold = 10;
                maxGold = 20;
                break;
            case ChestType.Rare:
                minGold = 30;
                maxGold = 50;
                break;
            case ChestType.Epic:
                minGold = 60;
                maxGold = 100;
                break;
        }
    }

    private int GenerateRandomGoldAmount()
    {
        int goldAmount = Random.Range(minGold, maxGold + 1);
        return goldAmount;
    }
}
