using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.Extras;

public class VR_PlayerInitialize : MonoBehaviour
{
    public bool VR_enabled = false;
    public GameObject VRplayer;
    public SteamVR_LaserPointer VR_LP;
    public bool debuglog;

    // Start is called before the first frame update
    private void Start()
    {
        VRplayer = GameObject.Find("Player");
        if (debuglog)
        {
            try
            {
                Debug.Log("VRP= " + VRplayer.name);
            }
            catch
            {
                Debug.Log("no Player active");
            }
        }

        // Initialize VR Player
        if (VRplayer)
        {
            VR_enabled = true;
            if (debuglog)
                Debug.Log("player true");
        }

        if (!VR_enabled)
        {
            if (debuglog)
                Debug.Log("enable a player");
            VRplayer = FindInactiveObjectByName("Player");
            if (VRplayer != null)
            {
                VRplayer.SetActive(true);
                VR_enabled = true;
            }
        }

        // Set VR Player Components
        if (VR_enabled)
        {
            VR_LP = GameObject.Find("RightHand").GetComponent<SteamVR_LaserPointer>();

            if (debuglog)
                Debug.Log("scene=" + SceneManager.GetActiveScene().name);
            if (SceneManager.GetActiveScene().name == "SceneA")
            {
                if (debuglog)
                    Debug.Log("SceneA");

                VRplayer.GetComponent<CharacterController>().enabled = false;
                VRplayer.GetComponent<VR_PlayerController>().enabled = false;
                VRplayer.transform.position = new Vector3(-16.41f, -10f, 16f);
                VRplayer.transform.rotation = new Quaternion(0, -104.2f, 0, 0);
                VRplayer.transform.localScale = new Vector3(10f, 10f, 10f);

                VR_LP.enabled = true;
                VR_LP.color = Color.black;
                VR_LP.m_maxDist = 100f;
                VR_LP.m_dist = 100f;
                GameObject.Find("New Game Object").transform.GetChild(0).gameObject.SetActive(true);

                VRplayer.GetComponent<VR_UnlockUser>().enabled = true;
            }
            else if (SceneManager.GetActiveScene().name == "SceneB_competition")
            {
                if (debuglog)
                    Debug.Log("SceneB");

                VRplayer.GetComponent<CharacterController>().enabled = false;
                VRplayer.GetComponent<VR_PlayerController>().enabled = false;
                VRplayer.transform.position = new Vector3 (-1.15f, -1.1f, 2.15f);
                VRplayer.transform.rotation = new Quaternion (0, -180f, 0, 0);
                VRplayer.transform.localScale = new Vector3 (1f, 1f, 1f);
                VRplayer.GetComponent<CharacterController>().enabled = true;
                VRplayer.GetComponent<CharacterController>().center = new Vector3(0, -0.15f, 0);
                VRplayer.GetComponent<CharacterController>().radius = 0.01f;
                VRplayer.GetComponent<CharacterController>().height = 0.01f;    // a buggy value, should be height + radius * 2 = Step Offset
                VRplayer.GetComponent<VR_PlayerController>().enabled = true;

                VR_LP.enabled = true;
                VR_LP.color = Color.red;
                VR_LP.m_maxDist = 1.5f;
                VR_LP.m_dist = 1.5f;
                GameObject.Find("New Game Object").transform.GetChild(0).gameObject.SetActive(true);

                VRplayer.GetComponent<VR_UnlockUser>().enabled = false;
            }
            else if (SceneManager.GetActiveScene().name == "SceneB_cooperation")
            {
                if (debuglog)
                    Debug.Log("SceneC");

                VRplayer.GetComponent<CharacterController>().enabled = false;
                VRplayer.GetComponent<VR_PlayerController>().enabled = false;
                VRplayer.transform.position = new Vector3 (0, 0, -54.9f);
                VRplayer.transform.rotation = new Quaternion(0, 0, 0, 0);
                VRplayer.transform.localScale = new Vector3(6f, 6f, 6f);

                VR_LP.enabled = false;
                GameObject.Find("New Game Object").transform.GetChild(0).gameObject.SetActive(false);

                VRplayer.GetComponent<VR_UnlockUser>().enabled = false;
            }
            else if (SceneManager.GetActiveScene().name == "SceneB_exploration")
            {
                if (debuglog)
                    Debug.Log("SceneD");

                VRplayer.GetComponent<CharacterController>().enabled = false;
                VRplayer.GetComponent<VR_PlayerController>().enabled = false;
                VRplayer.transform.position = new Vector3(-0.6f, 11.84058f, 47.5f);
                VRplayer.transform.rotation = new Quaternion(0, 0, 0, 0);
                VRplayer.transform.localScale = new Vector3(1f, 1f, 1f);

                VR_LP.enabled = true;
                VR_LP.color = Color.red;
                VR_LP.m_maxDist = 100f;
                VR_LP.m_dist = 100f;
                GameObject.Find("New Game Object").transform.GetChild(0).gameObject.SetActive(true);

                VRplayer.GetComponent<VR_UnlockUser>().enabled = false;
            }
        }
    }

    public static GameObject FindInactiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>();
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
