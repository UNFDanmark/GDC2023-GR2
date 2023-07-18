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

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private ParticleSystem friendParticle;

    private bool hasDoneIt = false;
    private bool playedMelodyQueue = false;
    private int enemyNumber;
    private bool Caerl = false;
    private float timeSinceLastSound = 0;
    private bool isPlaying = false;
    bool lastBool = false;
    private List<AudioClip> globalSoundQueue = new List<AudioClip>();
    private InstumentController globalInstumentController;
    private List<int> noteNames = new List<int>();

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
                if (!playedMelodyQueue)
                {
                    print("melodiqueue");
                    globalSoundQueue = MelodyQueueing();
                    playedMelodyQueue = true;
                }
            }
        }else
        {
            playedMelodyQueue = false;
            agent.speed = 3;
        }

        if (playedMelodyQueue && globalSoundQueue.Count != 0)
        {
            bool caerl = globalInstumentController.Pulse();
            
            print(globalSoundQueue.Count);
            if (caerl && !lastBool)
            {
                print("Playing sound of length:" + ((globalInstumentController.inputLeniency) * globalInstumentController.pulseDeltaTime));
                audioSource.PlayOneShot(globalSoundQueue[0]);
                globalSoundQueue.RemoveAt(0);
                particle.startColor = globalInstumentController.colors[noteNames[0]];
                particle.Play();
                noteNames.RemoveAt(0);


            }
            lastBool = caerl;
        }
    }

    List<AudioClip> MelodyQueueing()
    {
        globalInstumentController = player.GetComponent<InstumentController>();
        List<AudioClip> banjoSFX = globalInstumentController.banjoSFX;
        List<AudioClip> soundQueue = new List<AudioClip>();
        noteNames.Clear();

        
        foreach (var note in melody.integerList)
        {
            soundQueue.Add(banjoSFX[note]);
            noteNames.Add(note);
        }
        return soundQueue;
        
    }

    
    public void Befriending()
    {
        friendParticle.Play();
    }
}
