using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.Extras;

public class VR_PlayerInitialize : MonoBehaviour
{
    public bool VR_enabled = false;
    public GameObject VR_player;
    public SteamVR_LaserPointer VR_LP;
    public bool debuglog;

    // Start is called before the first frame update
    private void Start()
    {
        if (debuglog)
        {
            try
            {
                Debug.Log("VRP= " + VR_player.name);
            }
            catch
            {
                Debug.Log("no Player active");
            }
        }

        // Initialize VR Player
        if (GameObject.Find("Player") != null)
        {
            if (debuglog)
                Debug.Log("already have Player");
            VR_player = GameObject.Find("Player");
            VR_enabled = true;
        }
        else
        {
            Debug.Log("no player, use specific one.");
            VR_player.SetActive(true);
            VR_enabled = true;
        }

        if (debuglog)
            Debug.Log("player rotation: " + VR_player.transform.rotation);

        // Set VR Player Components
        if (VR_enabled)
        {
            VR_LP = GameObject.Find("RightHand").GetComponent<SteamVR_LaserPointer>();

            if (debuglog)
                Debug.Log("scene=" + SceneManager.GetActiveScene().name);
            if (SceneManager.GetActiveScene().name == "SceneA")
            {
                if (debuglog)
                    Debug.Log("Init SceneA");

                VR_player.GetComponent<CharacterController>().enabled = false;
                VR_player.GetComponent<VR_PlayerController>().enabled = false;
                VR_player.transform.position = new Vector3(-16.41f, -10f, 16f);
                VR_player.transform.rotation = new Quaternion(0, 0.70711f, 0, 0.70711f);
                VR_player.transform.localScale = new Vector3(10f, 10f, 10f);

                VR_LP.enabled = true;
                VR_LP.color = Color.red;
                VR_LP.m_maxDist = 100f;
                VR_LP.m_dist = 100f;
                if (GameObject.Find("New Game Object") != null)
                    GameObject.Find("New Game Object").transform.GetChild(0).gameObject.SetActive(true);

                VR_player.GetComponent<VR_UnlockUser>().lp = VR_player.GetComponent<VR_CheckLongPress>();
                VR_player.GetComponent<VR_UnlockUser>().rightHandLaser = VR_LP;
                VR_player.GetComponent<VR_UnlockUser>().enabled = true;
            }
            else if (SceneManager.GetActiveScene().name == "SceneB_competition")
            {
                if (debuglog)
                    Debug.Log("Init SceneB_competition");

                VR_player.GetComponent<CharacterController>().enabled = false;
                VR_player.GetComponent<VR_PlayerController>().enabled = false;
                VR_player.transform.position = new Vector3 (-1.15f, -1.1f, 2.15f);
                VR_player.transform.rotation = new Quaternion (0, 0, 0, 0);
                VR_player.transform.localScale = new Vector3 (1f, 1f, 1f);
                VR_player.GetComponent<CharacterController>().enabled = true;
                VR_player.GetComponent<CharacterController>().center = new Vector3(0, -0.15f, 0);
                VR_player.GetComponent<CharacterController>().radius = 0.01f;
                VR_player.GetComponent<CharacterController>().height = 0.01f;    // a buggy value, should be height + radius * 2 = Step Offset
                VR_player.GetComponent<VR_PlayerController>().enabled = true;

                VR_LP.enabled = true;
                VR_LP.color = Color.red;
                VR_LP.m_maxDist = 1.5f;
                VR_LP.m_dist = 1.5f;
                if (GameObject.Find("New Game Object") != null)
                    GameObject.Find("New Game Object").transform.GetChild(0).gameObject.SetActive(true);

                VR_player.GetComponent<VR_UnlockUser>().enabled = false;
            }
            else if (SceneManager.GetActiveScene().name == "SceneB_cooperation")
            {
                if (debuglog)
                    Debug.Log("Init SceneB_cooperation");

                VR_player.GetComponent<CharacterController>().enabled = false;
                VR_player.GetComponent<VR_PlayerController>().enabled = false;
                VR_player.transform.position = new Vector3 (0, 0, -54.9f);
                VR_player.transform.rotation = new Quaternion(0, 1f, 0, 0);
                VR_player.transform.localScale = new Vector3(6f, 6f, 6f);

                VR_LP.enabled = false;
                if (GameObject.Find("New Game Object") != null)
                    GameObject.Find("New Game Object").transform.GetChild(0).gameObject.SetActive(false);

                VR_player.GetComponent<VR_UnlockUser>().enabled = false;
            }
            else if (SceneManager.GetActiveScene().name == "SceneB_exploration")
            {
                if (debuglog)
                    Debug.Log("Init SceneB_exploration");

                VR_player.GetComponent<CharacterController>().enabled = false;
                VR_player.GetComponent<VR_PlayerController>().enabled = false;
                VR_player.GetComponent<VR_PlayerPointer>().enabled = true;
                VR_player.transform.position = new Vector3(3.22f, 10.85f, 46.7f);
                VR_player.transform.rotation = new Quaternion(0, -1f, 0, 0);
                VR_player.transform.localScale = new Vector3(1f, 1f, 1f);

                VR_LP.enabled = true;
                VR_LP.color = Color.red;
                VR_LP.m_maxDist = 100f;
                VR_LP.m_dist = 100f;
                if(GameObject.Find("New Game Object") != null)
                    GameObject.Find("New Game Object").transform.GetChild(0).gameObject.SetActive(true);
                VR_player.GetComponent<VR_PlayerPointer>().m_explore = GameObject.Find("ExploreNode").transform.GetComponent<ExploreUI>();

                VR_player.GetComponent<VR_UnlockUser>().enabled = false;
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
