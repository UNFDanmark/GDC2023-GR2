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
    Color[] colors;

    [SerializeField]
    float cooldownTime;

    [SerializeField] 
    private ParticleSystem particle;
    
    [SerializeField]
    private List<int> banjoSFX = new List<int>();

    float timeLeft;
    
    
    public List<PreDefinedNotes> chordDefinitions;
    public List<int> latestNotes = new List<int>();
    

    public List<PreDefinedNotes> enemyMelody;
    public List<GameObject> enemyGameObjects;


    void NoteInput() {
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
            //AudioSource.PlayClipAtPoint(banjoSFX[0],transform.position);
        }
        if (Input.GetKeyUp("u")) { images[0].color = Color.white; }

        if (Input.GetKeyDown("i"))
        {
            images[1].color = colors[1];
            AddToList(1);
            particle.startColor = colors[1];
            particle.Play();
        }
        if (Input.GetKeyUp("i")) { images[1].color = Color.white; }

        if (Input.GetKeyDown("o"))
        {
            images[2].color = colors[2];
            AddToList(2);
            particle.startColor = colors[2];
            particle.Play();
        }
        if (Input.GetKeyUp("o")) { images[2].color = Color.white; }

        if (Input.GetKeyDown("p"))
        {
            images[3].color = colors[3];
            AddToList(3);
            particle.startColor = colors[3];
            particle.Play();
        }
        if (Input.GetKeyUp("p")) { images[3].color = Color.white; }

        if (Input.GetKeyDown("j"))
        {
            images[4].color = colors[4];
            AddToList(4);
            particle.startColor = colors[4];
            particle.Play();
        }
        if (Input.GetKeyUp("j")) { images[4].color = Color.white; }

        if (Input.GetKeyDown("k"))
        {
            images[5].color = colors[5];
            AddToList(5);
            particle.startColor = colors[5];
            particle.Play();
        }
        if (Input.GetKeyUp("k")) { images[5].color = Color.white; }

        if (Input.GetKeyDown("l"))
        {
            images[6].color = colors[6];
            AddToList(6);
            particle.startColor = colors[6];
            particle.Play();
        }
        if (Input.GetKeyUp("l")) { images[6].color = Color.white; }

        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            images[7].color = colors[7];
            AddToList(7);
            particle.startColor = colors[7];
            particle.Play();
        }
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

    void Update()
    {
        NoteInput();
    }

}
