using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.Extras;

public class VR_PlayerDig : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public SteamVR_Action_Boolean inputAction;
    public bool selected;

    void Update()
    {
        //if (Input.GetButtonDown("XRI_Right_TriggerButton") || Input.GetButtonDown("XRI_Right_GripButton"))
        //if (inputAction.GetState(SteamVR_Input_Sources.RightHand))
        //{
        //    DigTree();
        //}
    }


    void DigTree()
    {
        //Debug.Log("Dig~");
        // point at tree?
        // do something... ***no raycast in PC, so use this instead***
        //GameObject obj = GameObject.Find("DevilTree");

        // I found a tree?
        //if (choosed.choosedObject != null)
        //{
        //    if (choosed.choosedObject.CompareTag("Devil Trees"))
        //    {
        //        // dig it!
        //        choosed.choosedObject.GetComponent<DevilTreeHandler>().OnDig();
        //    }
        //}
    }


    public void OnPointerClick(PointerEventData eventData)  // dig tree
    {
        Debug.Log("Click");
        DigTree();
    }

    public void OnPointerEnter(PointerEventData eventData)  // glow tree
    {
        Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)   // de-glow tree
    {
        Debug.Log("Exit");
    }
}
