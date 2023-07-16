using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [SerializeField] private Transform rig;

    [SerializeField] private float offsetY;
    [SerializeField] private float offsetZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(rig.position.x, rig.position.y + offsetY, rig.position.z + offsetZ);
    }
}
