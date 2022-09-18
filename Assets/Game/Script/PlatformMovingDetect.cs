using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit3D.GameCommands;

public class PlatformMovingDetect : MonoBehaviour
{
    public Vector3 originPosition, deltaPosition;
    protected Platform platform;

    private void Start()
    {
        platform = GetComponentInChildren<Platform>();
    }

    // Update is called once per frame
    void Update()
    {
        deltaPosition = transform.position - originPosition;
        originPosition = transform.position;
        //Debug.Log("plat: "+platform.transform.name);
        platform.MoveCharacterController(deltaPosition);
    }
}
