using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    

    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private Rigidbody rb;

    private float x;
    private float y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(playerSpeed * x, 0, playerSpeed * y);
    }
}
