using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ResetItself : MonoBehaviour
{
    private Vector3 originalPos;
    private CharacterController characterController;

    private void Start()
    {
        originalPos = GameObject.Find("Fox").GetComponent<Transform>().localPosition;
        characterController = GameObject.Find("Fox").GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Fox")
        {
            characterController.enabled = false;
            other.transform.localPosition = originalPos;
            characterController.enabled = true;
        }
        if(other.name == "Player")
        {
            characterController.enabled = false;
            other.transform.localPosition = new Vector3(-1.15f, -0.835f, 2.15f);
            characterController.enabled = true;
        }
    }
}
