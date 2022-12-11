using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VR_CheckLongPress : MonoBehaviour
{
    public SteamVR_Action_Boolean gripAction;

    protected bool longPress = false;
    [SerializeField]
    public float pressTriggerTime = 2.0f;
    public float pressTime = 0;

    public bool CheckButton()
    {
        //if (Input.GetButton("XRI_Right_TriggerButton") || Input.GetButton("XRI_Right_GripButton"))
        if (gripAction.GetState(SteamVR_Input_Sources.Any))
        {
            Debug.Log("checking...");
            pressTime += Time.deltaTime;
            if (pressTime >= pressTriggerTime)
            {
                Debug.Log("checked!");
                longPress = true;
            }
        }
        else
        {
            pressTime = 0;
            longPress = false;
        }

        return longPress;
    }
}
