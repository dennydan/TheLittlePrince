using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLongPress : MonoBehaviour
{
    protected bool longPress = false;
    [SerializeField]
    public float pressTriggerTime = 2.0f;
    public float pressTime = 0;

    public bool CheckButton()
    {
        if (Input.GetButton("Fire1"))
        {
            pressTime += Time.deltaTime;
            if (pressTime >= 2.0f)
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
