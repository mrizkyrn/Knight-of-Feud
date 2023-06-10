using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SubMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] menuContents;
    [SerializeField] private Image[] menuImages;
    [SerializeField] private float buttonPressOpacity = 0f;
    [SerializeField] private float defaultOpacity = 1f;

    private void Start()
    {
        // Set the initial opacity for each menu image
        for (int i = 0; i < menuImages.Length; i++)
        {
            SetImageOpacity(menuImages[i], defaultOpacity);
        }
        
        SetImageOpacity(menuImages[0], buttonPressOpacity);
    }

    public void ActivateMenu(int index)
    {
        // Set the content of the selected menu to active
        for (int i = 0; i < menuContents.Length; i++)
        {
            menuContents[i].SetActive(i == index);
        }

        // Set the opacity for each menu image
        for (int i = 0; i < menuImages.Length; i++)
        {
            SetImageOpacity(menuImages[i], i == index ? buttonPressOpacity : defaultOpacity);
        }
    }

    private void SetImageOpacity(Image image, float opacity)
    {
        Color imageColor = image.color;
        imageColor.a = opacity;
        image.color = imageColor;
    }
}
