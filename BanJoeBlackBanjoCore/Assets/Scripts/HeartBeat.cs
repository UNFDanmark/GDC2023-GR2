using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeat : MonoBehaviour
{
    [SerializeField] 
    private AudioSource audioSourceTemp;

    public bool pendingHeal;
    
    public void Play()
    {
        audioSourceTemp.Play();
    }

    
}
