using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        Debug.Log("New Game");
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
