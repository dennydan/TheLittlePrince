using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandOnPlatform : MonoBehaviour
{
    bool TPable = false;
    //Vector3 translation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("TP enable");
            Debug.Log("IN");

            // moving with platform +++
            other.gameObject.transform.SetParent(transform.parent);
            // moving with platform ---

            TPable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("TP disable");
            Debug.Log("OUT");

            // moving with platform +++
            other.gameObject.transform.SetParent(null);
            // moving with platform ---

            TPable = false;
        }
    }

    private void Update()
    {
        if(Input.GetButton("Fire2"))
        {
            if(TPable)
            {
                GameObject obj = GameObject.Find("XR Origin");  //For simulator
                obj.transform.position = transform.position + new Vector3(0, 0.8f, 0);
            }
        }
    }
}
