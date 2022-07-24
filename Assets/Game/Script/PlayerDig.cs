using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDig : MonoBehaviour
{
    // +++long press demo+++
    protected bool longPress = false;
    [SerializeField]
    public float pressTriggerTime = 2.0f;
    public float pressTime = 0;
    // ---long press demo---

    //public SteamVR_Action_Boolean snapLeftAction = SteamVR_Input.GetBooleanAction("SnapTurnLeft");

    // Update is called once per frame
    void Update()
    {
        // +++long press demo+++
        //if (CheckLongPress())
        //    DigTree();
        // ---long press demo---

        if (Input.GetButtonDown("XRI_Right_TriggerButton") || Input.GetButtonDown("XRI_Right_GripButton"))
        {
            DigTree();
        }
    }

    // +++long press demo+++
    public bool CheckLongPress()
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

    void DigTree()
    {
        //Debug.Log("Dig~");
        // point at tree?
        // do something... ***no raycast in PC, so use this instead***
        DevilTreeHandler obj = GameObject.Find("DevilTree").GetComponent<DevilTreeHandler>();

        // dig it
        obj.OnDig();
    }
    // ---long press demo---
}
