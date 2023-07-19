using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float interactDistance;
    [SerializeField] GameObject lorebook;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position )<interactDistance)
        {
            print("f");
            if (Input.GetKeyDown(KeyCode.E))
            {
                lorebook.SetActive(true);
            }
        }
    }
}
