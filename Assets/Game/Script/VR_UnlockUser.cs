using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VR_UnlockUser : MonoBehaviour
{
    private GameObject PC_User;
    private VR_CheckLongPress lp;

    public XRRayInteractor rightHandRay;
    public RaycastHit rayHit;

    private void Start()
    {
        //PC_User = GameObject.Find("Ellen");
        lp = GetComponent<VR_CheckLongPress>();

        rightHandRay = GameObject.Find("RightHand Controller").GetComponent<XRRayInteractor>();
    }

    // Update is called once per frame
    private void Update()
    {
        // unlock PC user
        if(lp.CheckButton())
        {
            rightHandRay.TryGetCurrent3DRaycastHit(out rayHit);
            //PC_User.GetComponent<PlayerInput>().enabled = true;
            if (rayHit.transform.gameObject.layer == 7)
            {
                Debug.Log("PC unlocked!");
                rayHit.transform.GetComponent<PlayerInput>().enabled = true;
            }

            // There is no use for this function, Delete itself.
            Destroy(GetComponent<VR_UnlockUser>());
        }
    }
}
