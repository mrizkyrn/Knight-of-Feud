using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectedItem : MonoBehaviour
{
    [SerializeField] private GameObject collectedItemPrefab;
    [SerializeField] private float displayDuration;

    public static CollectedItem Instance { get; private set; }
    private List<Item> Items = new List<Item>();
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetCollectItemUI(Sprite icon, string name, string amount)
    {
        GameObject GO = Instantiate(collectedItemPrefab, transform);
        Image itemIcon = GO.transform.Find("Image").GetComponent<Image>();
        TMP_Text itemName = GO.GetComponentInChildren<TMP_Text>();
        TMP_Text itemAmount = GO.GetComponentsInChildren<TMP_Text>()[1];

        itemIcon.sprite = icon;
        itemName.text = name;
        itemAmount.text = "x " + amount;

        StartCoroutine(DestroyUIObject(GO, displayDuration));
    }

    private IEnumerator DestroyUIObject(GameObject GO, float displayDuration)
    {
        yield return new WaitForSeconds(displayDuration);
        Destroy(GO);
    }
}
