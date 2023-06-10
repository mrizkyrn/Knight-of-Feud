using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private Image Weaponimage;
    [SerializeField] private PlayerStatsUI playerStatsUI;

    public Item item;

    private void Start()
    {
        AddWeapon(item);
    }

    public void AddWeapon(Item newItem)
    {
        PlayerStats.Instance.Damage.Decrease(item.damageWeapon);
        item = newItem;
        Weaponimage.sprite = newItem.icon;
        PlayerStats.Instance.Damage.Increase(item.damageWeapon);
        playerStatsUI.SetStats();
    }
}
