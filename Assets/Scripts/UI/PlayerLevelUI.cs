using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUI : MonoBehaviour
{
    [SerializeField] private Image xpBarFill;
    // [SerializeField] private Canvas xpBar;
    [SerializeField] private TMP_Text levelTxt;

    private void Update()
    {
        xpBarFill.fillAmount = PlayerStats.Instance.XP.CurrentValue/PlayerStats.Instance.XP.MaxValue;
    }

    private void Start()
    {
        PlayerStats.Instance.OnLevelUp += SetLevelUI;
    }

    private void OnDisable()
    {
        PlayerStats.Instance.OnLevelUp -= SetLevelUI;
    }

    private void SetLevelUI()
    {
        levelTxt.text = PlayerStats.Instance.Level.ToString();
    }
}
