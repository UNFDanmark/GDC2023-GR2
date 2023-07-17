using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PreDefinedNotes
{
    public List<int> integerList;
}

public class InstumentController : MonoBehaviour
{
    [SerializeField] private playerController player;
    [SerializeField]
    Image[] images;

    [SerializeField]
    public Color[] colors;

    [SerializeField]
    float cooldownTime;

    [SerializeField] 
    private ParticleSystem particle;
    
    [SerializeField]
    public List<AudioClip> banjoSFX = new List<AudioClip>();

    [SerializeField] 
    private AudioSource audioSource;
    
    [SerializeField]
    private AudioSource audioSourceTemp;

    [SerializeField] 
    private float BPM;

    [SerializeField] 
    public float pulseDeltaTime;
    
    [SerializeField] 
    public float clock;

    [SerializeField] 
    public float inputLeniency;

    [SerializeField] private Animator heartPulse;
    float timeLeft;
    
    
    
    public List<PreDefinedNotes> chordDefinitions;
    public List<int> latestNotes = new List<int>();
    

    public List<PreDefinedNotes> enemyMelody;
    public List<GameObject> enemyGameObjects;


    void Start()
    {
        pulseDeltaTime = 60 / BPM;

    }

    private void Awake()
    {
        AnimationClip heartAnimation = heartPulse.runtimeAnimatorController.animationClips[0];
        //heartAnimation.frameRate = 60;

        heartPulse.speed = BPM/60;
    }

    void NoteInput(bool pulse)
    { 
    if(pulse)
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            latestNotes.Clear();
        }

        if (Input.GetKeyDown("u"))
        {
            images[0].color = colors[0];
            AddToList(0);
            particle.startColor = colors[0];
            particle.Play();
            audioSource.clip = banjoSFX[0];
            audioSource.PlayOneShot(banjoSFX[0]);
        }
        if (Input.GetKeyDown("i"))
        {
            images[1].color = colors[1];
            AddToList(1);
            particle.startColor = colors[1];
            particle.Play();
            audioSource.clip = banjoSFX[1];
            audioSource.PlayOneShot(banjoSFX[1]);
        }
        if (Input.GetKeyDown("o"))
        {
            images[2].color = colors[2];
            AddToList(2);
            particle.startColor = colors[2];
            particle.Play();
            audioSource.clip = banjoSFX[2];
            audioSource.PlayOneShot(banjoSFX[2]);
        }
        if (Input.GetKeyDown("p"))
        {
            images[3].color = colors[3];
            AddToList(3);
            particle.startColor = colors[3];
            particle.Play();
            audioSource.clip = banjoSFX[3];
            audioSource.PlayOneShot(banjoSFX[3]);
        }
        if (Input.GetKeyDown("j"))
        {
            images[4].color = colors[4];
            AddToList(4);
            particle.startColor = colors[4];
            particle.Play();
            audioSource.clip = banjoSFX[4];
            audioSource.PlayOneShot(banjoSFX[4]);
        }
        if (Input.GetKeyDown("k"))
        {
            images[5].color = colors[5];
            AddToList(5);
            particle.startColor = colors[5];
            particle.Play();
            audioSource.clip = banjoSFX[5];
            audioSource.PlayOneShot(banjoSFX[5]);
        }
        if (Input.GetKeyDown("l"))
        {
            images[6].color = colors[6];
            AddToList(6);
            particle.startColor = colors[6];
            particle.Play();
            audioSource.clip = banjoSFX[6];
            audioSource.PlayOneShot(banjoSFX[6]);
        }
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            images[7].color = colors[7];
            AddToList(7);
            particle.startColor = colors[7];
            particle.Play();
            audioSource.clip = banjoSFX[7];
            audioSource.PlayOneShot(banjoSFX[7]);
        }
    }

        if (Input.GetKeyUp("u")) { images[0].color = Color.white; }
        if (Input.GetKeyUp("i")) { images[1].color = Color.white; }
        if (Input.GetKeyUp("o")) { images[2].color = Color.white; }
        if (Input.GetKeyUp("p")) { images[3].color = Color.white; }
        if (Input.GetKeyUp("j")) { images[4].color = Color.white; }
        if (Input.GetKeyUp("k")) { images[5].color = Color.white; }
        if (Input.GetKeyUp("l")) { images[6].color = Color.white; }
        if (Input.GetKeyUp(KeyCode.Semicolon)) { images[7].color = Color.white; }
        

        if (Input.GetKeyDown(KeyCode.RightShift)) { ChordCheck(); }
        if (Input.GetKeyDown(KeyCode.RightControl)){MelodyCheck();}
    }
    void AddToList(int i)
    {
        latestNotes.Add(i);
        if (latestNotes.Count > 7)
        {
            latestNotes.RemoveAt(0);
        }
        timeLeft = cooldownTime;
        
    }

    void ChordCheck() {
        try
        {
            int j = 0;
            foreach (var chord in chordDefinitions)
            {
                int i = 0;
                foreach (var note in chord.integerList)
                {
                    if (note != latestNotes[i])
                    {
                        break;
                    }
                    if (i == chord.integerList.Count - 1)
                    {
                        player.pendingAction = (ActionType)j+1;
                    }
                    i++;
                }
                timeLeft = 0;
            }
            j++;
        }
        catch (Exception e)
        {
            ;
        }
    }

    void MelodyCheck()
    {
        int j = 0;
        foreach (var melody in enemyMelody)
        {
            int i = 0;
            foreach (var note in melody.integerList)
            {
                if (note != latestNotes[i])
                {
                    break;
                }
                if (i == enemyMelody.Count-1)
                {
                    print("yeay");
                }

                i++;
            }

            timeLeft = 0;
        }

        j++;


    }

    public bool Pulse()
    {
        clock += Time.deltaTime;
        if (clock%pulseDeltaTime < 0+(inputLeniency/2)*pulseDeltaTime || clock%pulseDeltaTime > pulseDeltaTime-(inputLeniency/2)*pulseDeltaTime )
        {
            return true;
        }
        return false;
    }

    void Update()
    {
        NoteInput(Pulse());
    }

}
