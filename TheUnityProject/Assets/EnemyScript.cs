using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public List<int> melody;

    public List<float> rhythm;
    
    [SerializeField] private Transform player;
    [SerializeField] private float encounterDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < encounterDistance)
        {
            player.GetComponent<InstumentController>().enemyMelody = melody;
            player.GetComponent<InstumentController>().enemyRhythm = rhythm;
        }
    }
}
