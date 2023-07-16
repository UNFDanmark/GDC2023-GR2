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
    [SerializeField] private Transform oneDirection;
    [SerializeField] private LayerMask layerMask;

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

        agent.SetDestination(player.position);
        print(agent.destination);
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, stopDistance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("heaofjoiuewsrgf");
        }else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    void Befriending()
    {
        ;
    }
}
