using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public Sprite icon;
    public string itemName;
    [TextArea] public string description;
    public float chance;

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
