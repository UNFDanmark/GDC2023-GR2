using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rick : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up, 2);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {

            SceneManager.LoadScene("EndCredits");
            Destroy(gameObject);
        }
    }
}
