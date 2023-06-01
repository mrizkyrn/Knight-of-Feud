using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private TMP_Text goldTxt;

    private void Update()
    {
        goldTxt.text = PlayerStats.Instance.Gold.CurrentValue.ToString();
    }
}
