using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public PreDefinedNotes melody;

    public int HP = 12;

    private List<PreDefinedNotes> enemyMelodies = new List<PreDefinedNotes>();
    private List<GameObject> enemyGameObjects = new List<GameObject>();
    
    [SerializeField] private Transform player;
    [SerializeField] private float encounterDistance;
    [SerializeField] private float activeDistance;
    [SerializeField] private float stopDistance;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private ParticleSystem friendParticle;
    [SerializeField] private playerController playercontroller;
    [SerializeField] private float speed;
    [SerializeField] private float moveBackDistance;

    private bool triggerStay = true;
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
    public bool inactive = true;
    private bool paralyzed = false;
    private bool isTargetingPlayer = true;
    private bool beFriended = false;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        enemyMelodies = player.GetComponent<InstumentController>().enemyMelody;
        enemyGameObjects = player.GetComponent<InstumentController>().enemyGameObjects;
        playercontroller = player.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        Vector3 dif = transform.position - agent.destination;
        dif.y = 0;
        if (dif.magnitude < 1)
        {
            isTargetingPlayer = true;
        }
        if (HP <= 0)
        {
            playercontroller.chordPoints += 1;
            Destroy(gameObject);
        }
        if (inactive)
        {
            if (Vector3.Distance(transform.position, player.position) < activeDistance && !paralyzed)
            {
                inactive = false;
            }
        }
        else
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
            else
            {
                if (hasDoneIt)
                { 
                    enemyMelodies.Remove(melody);
                enemyGameObjects.Remove(gameObject);
                hasDoneIt = false;
                }
            }

            if (isTargetingPlayer)
            {
                agent.SetDestination(player.position);
            }

            if (isTargetingPlayer)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
                        stopDistance, layerMask))
                {
                    if (hit.transform.gameObject.layer == 6)
                    {
                        agent.speed = 0;
                        animator.SetBool("isWalking", false);
                        transform.LookAt(player.position);
                        if (!playedMelodyQueue)
                        {
                            globalSoundQueue = MelodyQueueing();
                            playedMelodyQueue = true;
                        }
                    }
                }
                else
                {
                    playedMelodyQueue = false;
                    agent.speed = speed;
                    animator.SetBool("isWalking", true);
                }
            }

            if (playedMelodyQueue && globalSoundQueue.Count != 0)
            {
                bool caerl = globalInstumentController.Pulse();


                if (caerl && !lastBool)
                {
                    animator.SetTrigger("strum");
                    audioSource.PlayOneShot(globalSoundQueue[0]);
                    globalSoundQueue.RemoveAt(0);
                    particle.startColor = globalInstumentController.colors[noteNames[0]];
                    particle.Play();
                    noteNames.RemoveAt(0);
                    if (globalSoundQueue.Count == 0)
                    {
                        playercontroller.Damage();
                        isTargetingPlayer = false;
                        Vector3 P2E = transform.position - player.position;
                        Vector3 push = P2E.normalized * moveBackDistance;
                        agent.SetDestination(transform.position+push);
                        agent.speed = speed;
                        animator.SetBool("isWalking", true);
                    }

                }

                lastBool = caerl;
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if (!beFriended)
        {
            if (other.CompareTag("Fireball"))
            {
                HP -= 6;
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Icewave"))
            {
                StartCoroutine(IceWave());
            }

            if (other.CompareTag("SleepField"))
            {
                print("ikew");
                StartCoroutine(SleepField());

            }
        }
    }

    
    
    IEnumerator IceWave()
    {
        HP -= 3;
        speed /= 2;
        yield return new WaitForSeconds(5);
        speed *= 2;

    }

    
    IEnumerator SleepField()
    {
        paralyzed = true;
        inactive = true;
        yield return new WaitForSeconds(5);
        paralyzed = false;
    }
    
    public void StartSleep()
    {
        StartCoroutine(SleepField());
    }
    public void Befriending()
    {
        if (!inactive)
        {
            friendParticle.Play();
            playercontroller.Heal();
            inactive = true;
            paralyzed = true;
            beFriended = true;
            Destroy(gameObject, 10);
        }
    }
}
