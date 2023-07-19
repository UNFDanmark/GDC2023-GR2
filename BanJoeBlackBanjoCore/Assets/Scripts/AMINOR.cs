using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMINOR : MonoBehaviour
{

    [SerializeField] private GameObject AMinor;
    [SerializeField] private InstumentController instumentController;
    
    void Update()
    {
        transform.Rotate(Vector3.up, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            instumentController.availableChords[1] = true;
            AMinor.SetActive(true);
            Destroy(gameObject);
        }
    }
}
