using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    SphereCollider colliderPosition;

    private void Start()
    {
        colliderPosition = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("XR Origin");  //For simulator
            try
            {
                obj = GameObject.Find("Player");  //For SteamVR
            }
            catch (System.Exception)
            {
                Debug.Log("No VR player");
                throw;
            }
            obj.transform.position = transform.GetChild(0).transform.position + colliderPosition.center + new Vector3(0, 2.8f, 0);
        }
    }
}
