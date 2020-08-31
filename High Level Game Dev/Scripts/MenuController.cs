using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject controlPanel, MainPanel;

    public Button playButton, exitButton;
    private void Start()
    {
        controlPanel.gameObject.SetActive(false);
    }

    public void pressedExited() {
        Debug.Log("exit");
        Application.Quit();
    }
    public void pressedPlay() {
        SceneManager.LoadScene(1);
       }
    public void pressedControl()
    {
        //implement to show the control of the music and volume of the game

        controlPanel.gameObject.SetActive(true);
        MainPanel.gameObject.SetActive(false);   
    }
    public void pressedBack()
    {   controlPanel.gameObject.SetActive(false);
        MainPanel.gameObject.SetActive(true);
    }

}
