using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstumentController : MonoBehaviour
{

    [SerializeField]
    Image[] images;

    [SerializeField]
    Color[] colors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("u"))
        {
            images[0].color = colors[0];
        } 
        if (Input.GetKeyUp("u")){ images[0].color = Color.white; }

        if (Input.GetKeyDown("i"))
        {
            images[1].color = colors[1];
        }
        if (Input.GetKeyUp("i")) { images[1].color = Color.white; }

        if (Input.GetKeyDown("o"))
        {
            images[2].color = colors[2];
        }
        if (Input.GetKeyUp("o")) { images[2].color = Color.white; }

        if (Input.GetKeyDown("p"))
        {
            images[3].color = colors[3];
        }
        if (Input.GetKeyUp("p")) { images[3].color = Color.white; }

        if (Input.GetKeyDown("j"))
        {
            images[4].color = colors[4];
        }
        if (Input.GetKeyUp("j")) { images[4].color = Color.white; }

        if (Input.GetKeyDown("k"))
        {
            images[5].color = colors[5];
        }
        if (Input.GetKeyUp("k")) { images[5].color = Color.white; }

        if (Input.GetKeyDown("l"))
        {
            images[6].color = colors[6];
        }
        if (Input.GetKeyUp("l")) { images[6].color = Color.white; }

        if (Input.GetKeyDown(KeyCode.Semicolon)){
            images[7].color = colors[7];
        }
        if (Input.GetKeyUp(KeyCode.Semicolon)) { images[7].color = Color.white; }

    }
}
