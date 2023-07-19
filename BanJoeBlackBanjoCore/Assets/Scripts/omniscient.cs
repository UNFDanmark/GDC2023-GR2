using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class omniscient : MonoBehaviour
{
    // Start is called before the first frame update

    public int hitPoints = 5;
    [FormerlySerializedAs("killCOunt")] public int killCount = 0;
    
    [SerializeField]
    public float leniancy = 0.4f;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Main Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
