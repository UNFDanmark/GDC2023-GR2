using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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

    [SerializeField] private float[] coolDownValues;
    [SerializeField] private List<float> coolDowns;
    [SerializeField] private Image[] coolDownUI;
    private List<Image> coolDownFillAmount;

    public Color[] colors;
    public Color[] passiveColor;

    [SerializeField]
    float cooldownTime;

    [SerializeField] 
    private ParticleSystem particle;
    
    [SerializeField]
    public List<AudioClip> banjoSFX = new List<AudioClip>();

    [SerializeField] 
    private AudioSource audioSource;
    
    

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
    public bool[] availableChords = new bool[4];
    public List<int> latestNotes = new List<int>();
    

    public List<PreDefinedNotes> enemyMelody;
    public List<GameObject> enemyGameObjects;

    private omniscient gameController;

    
    void Start()
    {
        pulseDeltaTime = 60 / BPM;
        gameController = GameObject.Find("gamecontroller").GetComponent<omniscient>();
        inputLeniency = gameController.leniancy;

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
        if (Input.GetKeyDown(KeyCode.RightShift)) { ChordCheck(); }
        if (Input.GetKeyDown(KeyCode.RightControl)){MelodyCheck();}

        if (Input.GetKeyUp("u")) { images[0].color = passiveColor[0]; }
        if (Input.GetKeyUp("i")) { images[1].color = passiveColor[1]; }
        if (Input.GetKeyUp("o")) { images[2].color = passiveColor[2]; }
        if (Input.GetKeyUp("p")) { images[3].color = passiveColor[3]; }
        if (Input.GetKeyUp("j")) { images[4].color = passiveColor[4]; }
        if (Input.GetKeyUp("k")) { images[5].color = passiveColor[5]; }
        if (Input.GetKeyUp("l")) { images[6].color = passiveColor[6]; }
        if (Input.GetKeyUp(KeyCode.Semicolon)) { images[7].color = passiveColor[7]; }
        

        
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
                if (!availableChords[j])
                {
                    continue;
                }
                
                foreach (var note in chord.integerList)
                {
                    if (note != latestNotes[i])
                    {
                        break;
                    }
                    if (i == chord.integerList.Count - 1)
                    {
                        if (coolDowns[j] <= 0)
                        {
                            player.pendingAction = (ActionType)j + 1;
                            coolDowns[j] = coolDownValues[j];
                        }
                    }
                    i++;
                }
                timeLeft = 0;
                j++;
            }
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
                    continue;
                }
                if (i == enemyMelody.Count)
                {
                    enemyGameObjects[j].GetComponent<EnemyScript>().Befriending();
                    enemyMelody.RemoveAt(j);
                    enemyGameObjects.RemoveAt(j);
                }

                i++;
            }

            timeLeft = 0;
            
        j++;
        }
    }

    public bool Pulse()
    {
        if (clock%pulseDeltaTime < 0+(inputLeniency/2)*pulseDeltaTime || clock%pulseDeltaTime > pulseDeltaTime-(inputLeniency/2)*pulseDeltaTime )
        {
            return true;
        }
        return false;
    }

    void Update()
    {
        for (int i = 0; i < coolDowns.Count; i++)
        {
            if (coolDowns[i] > 0)
            {
                coolDowns[i] -= Time.deltaTime;

                coolDownUI[i].fillAmount = coolDowns[i] / coolDownValues[i];
            }
        }
        
        clock += Time.deltaTime;
        NoteInput(Pulse());
        
    }

}
