using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private GameObject tutorialtext;
    [SerializeField] private GameObject startScreen;
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
}
