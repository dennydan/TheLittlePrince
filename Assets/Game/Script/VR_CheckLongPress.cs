using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_CheckLongPress : MonoBehaviour
{
    protected bool longPress = false;
    [SerializeField]
    public float pressTriggerTime = 2.0f;
    public float pressTime = 0;

    public bool CheckButton()
    {
        if (Input.GetButton("XRI_Right_TriggerButton") || Input.GetButton("XRI_Right_GripButton"))
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
