using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BowlingBallCollisionCheck : MonoBehaviour
{
    public UnityEvent AtBackwallEvent;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Backwall")
        {
            AtBackwallEvent.Invoke();

        }
    }
}
