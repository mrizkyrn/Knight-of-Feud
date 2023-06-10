using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthTxt;
    [SerializeField] private TMP_Text xpTxt;
    [SerializeField] private TMP_Text damageTxt;
    [SerializeField] private TMP_Text defenseTxt;
    [SerializeField] private TMP_Text shieldTxt;

    public void SetStats()
    {
        healthTxt.text = "Health : " + PlayerStats.Instance.Health.CurrentValue.ToString() + "/" + PlayerStats.Instance.Health.MaxValue.ToString();
        xpTxt.text = "XP : " + PlayerStats.Instance.XP.CurrentValue.ToString() + "/" + PlayerStats.Instance.XP.MaxValue.ToString();
        shieldTxt.text = "Shield : " + PlayerStats.Instance.ShieldDurability.CurrentValue.ToString() + "/" + PlayerStats.Instance.ShieldDurability.MaxValue.ToString();
        damageTxt.text = "Damage : " + PlayerStats.Instance.Damage.CurrentValue.ToString();
        defenseTxt.text = "Defense : " + PlayerStats.Instance.Defense.CurrentValue.ToString();
    }
}
