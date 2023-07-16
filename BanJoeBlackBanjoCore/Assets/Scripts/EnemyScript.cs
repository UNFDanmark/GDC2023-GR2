using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public PreDefinedNotes melody;

    private List<PreDefinedNotes> enemyMelodies = new List<PreDefinedNotes>();
    private List<GameObject> enemyGameObjects = new List<GameObject>();
    
    [SerializeField] private Transform player;
    [SerializeField] private float encounterDistance;

    private bool hasDoneIt = false;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyMelodies = player.GetComponent<InstumentController>().enemyMelody;
        enemyGameObjects = player.GetComponent<InstumentController>().enemyGameObjects;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < encounterDistance)
        {
            if (!hasDoneIt)
            {
                enemyMelodies.Add(melody);
                enemyGameObjects.Add(gameObject);
                hasDoneIt = true;
            }
        }
    }

    void Befriending()
    {
        ;
    }
}
