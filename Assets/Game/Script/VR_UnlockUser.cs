using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Valve.VR.Extras;

public class VR_UnlockUser : MonoBehaviour
{
    public VR_CheckLongPress lp;
    public SteamVR_LaserPointer rightHandLaser;
    bool CatchFox;

    private void Start()
    {
        //PC_User = GameObject.Find("Ellen");
        lp = GetComponent<VR_CheckLongPress>();

        rightHandLaser = GameObject.Find("RightHand").GetComponent<SteamVR_LaserPointer>();
        rightHandLaser.PointerIn += PointerIn;
        rightHandLaser.PointerOut += PointerOut;
        //rightHandLaser.PointerClick += PointerClick;
    }

    private void PointerIn(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Fox")
            CatchFox = true;
    }

    private void PointerOut(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Fox")
            CatchFox = false;
    }

    private void Update()
    {
        if (CatchFox && lp.CheckButton())
        {
            Debug.Log("PC unlocked!");
            GameObject.Find("Fox").GetComponent<PlayerInput>().enabled = true;
            // There is no use for this function, Delete itself.
            //Destroy(GetComponent<VR_UnlockUser>());
        }
    }
}
