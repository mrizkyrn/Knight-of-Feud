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
}
