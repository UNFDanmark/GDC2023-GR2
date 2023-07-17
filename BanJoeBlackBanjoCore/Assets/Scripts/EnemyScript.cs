using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public PreDefinedNotes melody;

    private List<PreDefinedNotes> enemyMelodies = new List<PreDefinedNotes>();
    private List<GameObject> enemyGameObjects = new List<GameObject>();
    
    [SerializeField] private Transform player;
    [SerializeField] private float encounterDistance;
    [SerializeField] private float stopDistance;
    [SerializeField] private NavMeshAgent agent;
    
    
    [SerializeField] private LayerMask layerMask;

    private bool hasDoneIt = false;
    private int enemyNumber;
    
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
                enemyNumber = enemyMelodies.Count;
                enemyMelodies.Add(melody);
                enemyGameObjects.Add(gameObject);
                hasDoneIt = true;
            }
        }
        else
        {
            enemyMelodies.RemoveAt(enemyNumber);
            hasDoneIt = false;
        }

        agent.SetDestination(player.position);
        
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, stopDistance, layerMask))
        {
            if (hit.transform.gameObject.layer == 6)
            {
                agent.speed = 0;
                transform.LookAt(player.position);
            }

        }else
        {
            agent.speed = 3;

        }
    }

    void Befriending()
    {
        ;
    }
}
