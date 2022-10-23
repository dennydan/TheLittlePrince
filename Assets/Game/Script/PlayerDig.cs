using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDig : MonoBehaviour
{
    public ChoosingFromRaycast choosed;

    private void Start()
    {
        choosed = GetComponent<ChoosingFromRaycast>();
    }

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
        //GameObject obj = GameObject.Find("DevilTree");

        // I found a tree?
        if(choosed.choosedObject != null)
        {
            if (choosed.choosedObject.CompareTag("Devil Trees"))
            {
                // dig it!
                choosed.choosedObject.GetComponent<DevilTreeHandler>().OnDig(this.name);
            }
        }
    }
}
