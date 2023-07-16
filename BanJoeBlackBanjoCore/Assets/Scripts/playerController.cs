using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    nothing,
    fireball,
    healing,
    damage
}

public class playerController : MonoBehaviour
{
    
    
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private Rigidbody rb;

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
            print("fuha");
            pendingAction = ActionType.nothing;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(x, 0 ,y).normalized * playerSpeed;
        
    }
}
