using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    public Vector3 pos;
    public Quaternion rot;
    public Rigidbody rb;
    public GameObject g;

    // Start is called before the first frame update
    void Start()
    {
        pos = this.transform.position;
        rot = this.transform.rotation;
        rb = this.GetComponent<Rigidbody>();
        

        this.GetComponent<BowlingBallCollisionCheck>().AtBackwallEvent.AddListener(delegate
        {
            g.SetActive(false);
        });
    }
}

