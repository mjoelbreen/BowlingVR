using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pin : MonoBehaviour
{
    public Vector3 pos;
    public Quaternion rot;
    public Rigidbody rb;
    public GameObject g;
    public PinCollisionCheck c;

    void Start()
    {
        pos = this.transform.position;
        rot = this.transform.rotation;
        rb = this.GetComponent<Rigidbody>();
        g = this.GetComponent<GameObject>();
        c = this.GetComponent<PinCollisionCheck>();
    }
   
}
