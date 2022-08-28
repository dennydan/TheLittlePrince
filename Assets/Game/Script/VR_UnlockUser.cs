using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_UnlockUser : MonoBehaviour
{
    private GameObject PC_User;
    private VR_CheckLongPress lp;

    private void Start()
    {
        PC_User = GameObject.Find("Ellen");
        lp = GetComponent<VR_CheckLongPress>();
    }

    // Update is called once per frame
    private void Update()
    {
        // unlock PC user
        if(lp.CheckButton())
        {
            Debug.Log("PC unlocked!");
            PC_User.GetComponent<PlayerInput>().enabled = true;

            // There is no use for this function, Delete itself.
            Destroy(GetComponent<VR_UnlockUser>());
        }
    }
}
