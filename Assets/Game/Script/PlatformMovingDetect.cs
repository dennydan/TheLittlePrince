using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit3D.GameCommands;

public class PlatformMovingDetect : MonoBehaviour
{
    public Vector3 originPosition, deltaPosition, newposition;
    //public Quaternion origiRotation, deltaRotation, newRotation;
    public Platform platform;

    protected CharacterController m_CharacterController;

    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            platform = other.GetComponent<Platform>();
            originPosition = platform.transform.position;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            if (platform != null)
            {
                newposition = platform.transform.position;
                deltaPosition = newposition - originPosition;
                originPosition = newposition;
                Debug.Log("move: " + deltaPosition);
                m_CharacterController.Move(deltaPosition);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            platform = null;
            originPosition = deltaPosition  = newposition = new Vector3(0,0,0);
        }
    }
}
