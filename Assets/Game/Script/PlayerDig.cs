using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDig : MonoBehaviour
{
    void Update()
    {
        //if (Input.GetButtonDown("XRI_Right_TriggerButton") || Input.GetButtonDown("XRI_Right_GripButton"))
        if (Input.GetButtonDown("Fire1"))
        {
            DigTree();
        }
    }


    void DigTree()
    {
        //Debug.Log("Dig~");
        // point at tree?
        // do something... ***no raycast in PC, so use this instead***
        GameObject obj = GameObject.Find("DevilTree");

        // find a tree?
        if (obj != null)
        {
            // dig it!
            obj.GetComponent<DevilTreeHandler>().OnDig();
        }
    }
}
