using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winzone : MonoBehaviour
{
    [SerializeField] private playerController PlayerController;
    [SerializeField] private int SceneNumber;
    [SerializeField] private InstumentController instumentController;
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "FinalTransition")
        {
            if (instumentController.availableChords[3] == true)
            {
                PlayerController.PrepareForNewScene();
                SceneManager.LoadScene(SceneNumber);
            }
        }
        else
        {
            PlayerController.PrepareForNewScene();
            SceneManager.LoadScene(SceneNumber);
        }
    }
}
