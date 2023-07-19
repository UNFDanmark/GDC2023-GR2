using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private GameObject tutorialtext;
    [SerializeField] private GameObject startScreen;
    private omniscient gameControl;

    private void Start()
    {
        gameControl = GameObject.Find("gamecontroller").GetComponent<omniscient>();
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Scene1");
    }
    
    public void QuitButton()
    {
        Application.Quit();
    }

    public void TutorialButton()
    {
        startScreen.SetActive(false);
        tutorialtext.SetActive(true);
    }

    public void MainMenuButton()
    {
        startScreen.SetActive(true);
        tutorialtext.SetActive(false);
    }

    public void Easy()
    {
        gameControl.leniancy = 0.6f;
    }
    public void Normal()
    {
        gameControl.leniancy = 0.4f;
    }
    public void Hard()
    {
        gameControl.leniancy = 0.2f;
    }
}
