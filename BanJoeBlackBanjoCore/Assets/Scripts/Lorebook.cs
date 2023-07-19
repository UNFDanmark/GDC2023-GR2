using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lorebook : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    [SerializeField] private GameObject loreBook;
    

    private int i = 0;
    public void PageForward()
    {
        if (i < pages.Length-1)
        {
            pages[i].SetActive(false);
            i++;
            pages[i].SetActive(true);
        }
        else
        {
            pages[i].SetActive(false);
            i = 0;
            pages[i].SetActive(true);
        }

    }

    public void PageBack()
    {
        if (i == 0)
        {
            pages[i].SetActive(false);
            i = pages.Length-1;
            pages[i].SetActive(true);
        }
        else
        {
            pages[i].SetActive(false);
            i--;
            pages[i].SetActive(true);
        }
    }

    public void Exit()
    {
        loreBook.SetActive(false);
    }
}
