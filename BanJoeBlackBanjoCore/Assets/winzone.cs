using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winzone : MonoBehaviour
{
    [SerializeField] private playerController PlayerController;
    [SerializeField] private int SceneNumber;
    private void OnTriggerEnter(Collider other)
    {
        print("gfj");
        PlayerController.PrepareForNewScene();
        SceneManager.LoadScene(SceneNumber);
    }
}
