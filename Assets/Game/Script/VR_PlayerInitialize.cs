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

    // Start is called before the first frame update
    private void Start()
    {
        VRplayer = GameObject.Find("Player");
        Debug.Log("VRP= " + VRplayer);
        if (VRplayer)
        {
            VR_enabled = true;
            Debug.Log("player true");
        }

        // Initialize VR Player
        if (!VR_enabled)
        {
            Debug.Log("enable a player");
            if (GameManager.FindInactiveObjectByName("Player") != null)
            {
                Debug.Log("player enable");
                GameManager.FindInactiveObjectByName("Player").SetActive(true);
                VR_enabled = true;
            }
        }
        // Set VR Player Components
        if (VR_enabled)
        {
            VR_LP = GameObject.Find("RightHand").GetComponent<SteamVR_LaserPointer>();

            Debug.Log("scene=" + SceneManager.GetActiveScene().name);
            if (SceneManager.GetActiveScene().name == "SceneA")
            {
                Debug.Log("SceneA");

                VRplayer.GetComponent<CharacterController>().enabled = false;
                VRplayer.GetComponent<VR_PlayerController>().enabled = false;
                VRplayer.transform.position = new Vector3(-16.41f, -10f, 16f);
                VRplayer.transform.rotation = new Quaternion(0, -104.2f, 0, 0);
                VRplayer.transform.localScale = new Vector3(10f, 10f, 10f);

                GameObject.Find("New Game Object").transform.GetChild(0).gameObject.SetActive(true);
                VR_LP.enabled = true;
                VR_LP.color = Color.black;
                VR_LP.m_maxDist = 100f;
                VR_LP.m_dist = 100f;

                VRplayer.GetComponent<CharacterController>().enabled = false;
                VRplayer.GetComponent<VR_PlayerController>().enabled = false;
                VRplayer.GetComponent<VR_UnlockUser>().enabled = true;
            }
            else if (SceneManager.GetActiveScene().name == "SceneB_competition")
            {
                Debug.Log("SceneB");

                VRplayer.GetComponent<CharacterController>().enabled = false;
                VRplayer.GetComponent<VR_PlayerController>().enabled = false;
                VRplayer.transform.position = new Vector3 (-1.15f, -1.1f, 2.15f);
                VRplayer.transform.rotation = new Quaternion (0, -180f, 0, 0);
                VRplayer.transform.localScale = new Vector3 (1f, 1f, 1f);

                GameObject.Find("New Game Object").transform.GetChild(0).gameObject.SetActive(true);
                VR_LP.enabled = true;
                VR_LP.color = Color.red;
                VR_LP.m_maxDist = 1.5f;
                VR_LP.m_dist = 1.5f;

                VRplayer.GetComponent<CharacterController>().enabled = true;
                VRplayer.GetComponent<VR_PlayerController>().enabled = true;
                VRplayer.GetComponent<VR_UnlockUser>().enabled = false;
                VRplayer.GetComponent<VR_PlayerController>().m_Animator = GameObject.Find("Prince").GetComponent<Animator>();
            }
            else if (SceneManager.GetActiveScene().name == "SceneB_cooperation")
            {
                Debug.Log("SceneC");

                VRplayer.GetComponent<CharacterController>().enabled = false;
                VRplayer.GetComponent<VR_PlayerController>().enabled = false;
                VRplayer.transform.position = new Vector3 (0, 0, -54.9f);
                VRplayer.transform.rotation = new Quaternion(0, 0, 0, 0);
                VRplayer.transform.localScale = new Vector3(6f, 6f, 6f);

                VR_LP.enabled = false;
                GameObject.Find("New Game Object").transform.GetChild(0).gameObject.SetActive(false);

                VRplayer.GetComponent<CharacterController>().enabled = false;
                VRplayer.GetComponent<VR_PlayerController>().enabled = false;
                VRplayer.GetComponent<VR_UnlockUser>().enabled = false;
            }
            else if (SceneManager.GetActiveScene().name == "SceneB_exploration")
            {
                Debug.Log("SceneD");

                VRplayer.GetComponent<CharacterController>().enabled = false;
                VRplayer.GetComponent<VR_PlayerController>().enabled = false;
                VRplayer.transform.position = new Vector3(-0.6f, 11.84058f, 47.5f);
                VRplayer.transform.rotation = new Quaternion(0, 0, 0, 0);
                VRplayer.transform.localScale = new Vector3(0, 0, 0);

                GameObject.Find("New Game Object").transform.GetChild(0).gameObject.SetActive(true);
                VR_LP.enabled = true;
                VR_LP.color = Color.red;
                VR_LP.m_maxDist = 100f;
                VR_LP.m_dist = 100f;

                VRplayer.GetComponent<CharacterController>().enabled = false;
                VRplayer.GetComponent<VR_PlayerController>().enabled = false;
                VRplayer.GetComponent<VR_UnlockUser>().enabled = false;
            }
        }
    }
}
