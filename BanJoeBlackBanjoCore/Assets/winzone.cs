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
    private omniscient Gamecontroller;

    private void Start()
    {
        Gamecontroller = GameObject.Find("gamecontroller").GetComponent<omniscient>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "FinalTransition")
        {
            if (instumentController.availableChords[3] == true)
            {
                Gamecontroller.hitPoints = PlayerController.HP;
                Gamecontroller.killCount = PlayerController.chordPoints;
                SceneManager.LoadScene(SceneNumber);
            }
        }
        else
        {
            Gamecontroller.hitPoints = PlayerController.HP;
            Gamecontroller.killCount = PlayerController.chordPoints;
            SceneManager.LoadScene(SceneNumber);
        }
    }
}
