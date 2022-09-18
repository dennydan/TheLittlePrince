using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VR_CheckLongPress : MonoBehaviour
{
    public SteamVR_Action_Boolean inputAction;

    protected bool longPress = false;
    [SerializeField]
    public float pressTriggerTime = 2.0f;
    public float pressTime = 0;

    public bool CheckButton()
    {
        //if (Input.GetButton("XRI_Right_Trigger") || Input.GetButton("XRI_Right_Grip"))
        if (inputAction.GetState(SteamVR_Input_Sources.RightHand))
        {
            pressTime += Time.deltaTime;
            if (pressTime >= pressTriggerTime)
            {
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
