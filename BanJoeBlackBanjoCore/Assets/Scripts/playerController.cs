using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ActionType
{
    nothing,
    fireball,
    sleep,
    ice
}

public class playerController : MonoBehaviour
{


    [SerializeField] private int HP;
    [SerializeField] private List<GameObject> hitPoints;
    [SerializeField] private InstumentController instumentController;
    
    
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField] 
    GameObject fireBall;
    [SerializeField] 
    GameObject shootingRig;
    [SerializeField] 
    private int fireBallSpeed;
    [SerializeField] 
    GameObject iceWave;
    [SerializeField] 
    GameObject fieldRig;
    [SerializeField] 
    private GameObject sleepField;
    [SerializeField] 
    private int fieldLifetime;

    [SerializeField] private float fireballLifetime;
    [SerializeField] private GameObject[] chordsUI;
    
    public float chordPoints;
    
    private float x;
    private float y;
    private float hypo;
    private float angle;
    private float lastAngle;

    public ActionType pendingAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (chordPoints < 1)
        {
            if (!instumentController.availableChords[0])
            {
                instumentController.availableChords[0] = true;
                chordsUI[0].SetActive(true);
            }
            
        } else if (chordPoints < 2)
        {
            if (!instumentController.availableChords[1])
            {
                instumentController.availableChords[1] = true;
                chordsUI[1].SetActive(true);
            }
        }
        else if (chordPoints < 3)
        {
            if (!instumentController.availableChords[2])
            {
                instumentController.availableChords[2] = true;
                chordsUI[2].SetActive(true);
            }
        }
        
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        hypo = MathF.Sqrt(x*x+y*y);
        angle = (Mathf.Acos(x / hypo)*180)/MathF.PI;
       
        if (y > 0)
        {
            angle *= -1;
        }
        if (!(x == 0 && y == 0))
        {
            lastAngle = angle;
        }
        transform.rotation = Quaternion.Euler(0, lastAngle, 0);

        if (pendingAction == ActionType.nothing)
        {
            ;
        } else if (pendingAction == ActionType.fireball)
        {
            FireBall();
            pendingAction = ActionType.nothing;
        } else if (pendingAction == ActionType.ice)
        {
            IceWave();
            pendingAction = ActionType.nothing;
        } else if (pendingAction == ActionType.sleep)
        {
            Sleep();
            pendingAction = ActionType.nothing;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(x, 0 ,y).normalized * playerSpeed;
        
    }

    public void Damage()
    {
        HP -= 1;
        
        hitPoints[HP].gameObject.GetComponent<Image>().enabled = false;
        if (HP <= 0)
        {
            SceneManager.LoadScene("DeathScreen");
        }
        
    }

    public void Heal()
    {
        if (HP < 5)
        {
            hitPoints[HP].gameObject.GetComponent<Image>().enabled = true;
            HP += 1;
            
        }
    }

    void FireBall()
    {
        GameObject fireBallInstance = Instantiate(fireBall, shootingRig.transform.position, transform.rotation);
        fireBallInstance.GetComponent<Rigidbody>().velocity = shootingRig.transform.forward*fireBallSpeed;
        Destroy(fireBallInstance, fireballLifetime);
    }

    void IceWave()
    {
        GameObject iceWaveInstance = Instantiate(iceWave, shootingRig.transform.position, transform.rotation);
        iceWaveInstance.GetComponent<Rigidbody>().velocity = shootingRig.transform.forward*fireBallSpeed;
        Destroy(iceWaveInstance, fireballLifetime);
    }

    void Sleep()
    {
        GameObject sleepInstance = Instantiate(sleepField, fieldRig.transform.position, transform.rotation);
        Collider[] hits = Physics.OverlapSphere(fieldRig.transform.position, 10);
        
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.gameObject.GetComponent<EnemyScript>().StartSleep();
            }
        }
        Destroy(sleepInstance, fieldLifetime);
    }

  
}
