using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public Affect affect;
    public Sprite icon;
    public string itemName;
    public int level;
    [TextArea] public string description;
    public float buyingPrice;
    public float damageWeapon;

    // Custom enum for item types
    public enum ItemType
    {
        Weapon,
        Potion
    }

    public enum Affect
    {
        Health,
        Damage,
        Defense
    }

    // // Additional function to perform the item's effect
    // public void PerformEffect()
    // {
    //     switch (itemType)
    //     {
    //         case ItemType.Potion:
    //             UsePotion();
    //             break;
    //         case ItemType.Weapon:
    //             UseWeapon();
    //             break;
    //         default:
    //             Debug.LogError("Unknown item type");
    //             break;
    //     }
    // }

    public void UsePotion()
    {
        if (affect == Affect.Health)
        {
            switch (level)
            {
                case 1: PlayerStats.Instance.Health.Increase(PlayerStats.Instance.Health.MaxValue * 0.25f); break;
                case 2: PlayerStats.Instance.Health.Increase(PlayerStats.Instance.Health.MaxValue * 0.50f); break;
                case 3: PlayerStats.Instance.Health.Increase(PlayerStats.Instance.Health.MaxValue * 1f); break;
                default: Debug.LogError("level item not assigned"); break;
            }
        }
        else if (affect == Affect.Damage)
        {
            // Perform damage potion effect
        }
        else if (affect == Affect.Defense)
        {
            // Perform defense potion effect
        }
        else
        {
            Debug.LogError("Unknown potion effect");
        }
    }

    // private void UseWeapon()
    // {
    //     // Perform weapon effect

    // }
}
