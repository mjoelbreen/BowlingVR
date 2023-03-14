using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PinCollisionCheck : MonoBehaviour
{
    public UnityEvent OnKnockedOverEvent;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            OnKnockedOverEvent.Invoke();
            
        }
    }
}
