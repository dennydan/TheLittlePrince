using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ResetItself : MonoBehaviour
{
    private Vector3 originalPos;

    private void Start()
    {
        originalPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("End"))
        {
            transform.position = originalPos;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }    
    }
}
