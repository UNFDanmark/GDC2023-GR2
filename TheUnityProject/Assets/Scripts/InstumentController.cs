using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PreDefinedChords
{
    public List<int> integerList;
}

public class InstumentController : MonoBehaviour
{

    [SerializeField]
    Image[] images;

    [SerializeField]
    Color[] colors;

    [SerializeField]
    float cooldownTime;

    float timeLeft;

    public List<PreDefinedChords> chordDefinitions;
    public List<int> latestNotes = new List<int>();
    
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
        }
        if (Input.GetKeyUp("u")) { images[0].color = Color.white; }

        if (Input.GetKeyDown("i"))
        {
            images[1].color = colors[1];
            AddToList(1);
        }
        if (Input.GetKeyUp("i")) { images[1].color = Color.white; }

        if (Input.GetKeyDown("o"))
        {
            images[2].color = colors[2];
            AddToList(2);
        }
        if (Input.GetKeyUp("o")) { images[2].color = Color.white; }

        if (Input.GetKeyDown("p"))
        {
            images[3].color = colors[3];
            AddToList(3);
        }
        if (Input.GetKeyUp("p")) { images[3].color = Color.white; }

        if (Input.GetKeyDown("j"))
        {
            images[4].color = colors[4];
            AddToList(4);
        }
        if (Input.GetKeyUp("j")) { images[4].color = Color.white; }

        if (Input.GetKeyDown("k"))
        {
            images[5].color = colors[5];
            AddToList(5);
        }
        if (Input.GetKeyUp("k")) { images[5].color = Color.white; }

        if (Input.GetKeyDown("l"))
        {
            images[6].color = colors[6];
            AddToList(6);
        }
        if (Input.GetKeyUp("l")) { images[6].color = Color.white; }

        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            images[7].color = colors[7];
            AddToList(7);
        }
        if (Input.GetKeyUp(KeyCode.Semicolon)) { images[7].color = Color.white; }

        if (Input.GetKeyDown(KeyCode.RightShift)) { ChordCheck(); }
    }
    void AddToList(int i)
    {
        latestNotes.Add(i);
        if (latestNotes.Count > 3)
        {
            latestNotes.RemoveAt(0);
        }
        timeLeft = cooldownTime;
        
    }

    void ChordCheck() {
    
    }

    void Update()
    {
        NoteInput();
    }

}
