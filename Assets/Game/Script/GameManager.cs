using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Gamekit3D.GameCommands;
using Valve.VR.Extras;

public class GameManager : MonoBehaviour
{
    enum LAYER
    {
        PC = 7,
        VR = 8,
    }
    
    // 改成單例模式
    public static GameManager Instance;

    //public TMP_Dropdown dropdown;
    //public int selectedLevel;

    GameObject moduleUI;

    public static int PC_tree, VR_tree;    // score board
    public static int end_point = 3;    // win point

    public static int stars;    // collected stars
    public static int stars_num = 6;    // Cooperation pass num

    public static int passedQuest;  // Exploration answered num, you're smart ass
    public static int pass_num = 12; // Exploration pass num

    public static bool competitionFinish = false;
    public static bool bMissionComlete = false;    // Current mission is done and show message.
    public static string version = "v0.7.1";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnStartGame(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Debug.Log("paused");
            //try
            //{
            //    moduleUI = FindInActiveObjectByName("ModuleUI");
            //}
            //catch
            //{
            //    Transform parent = GameObject.Find("Canvas").transform;
            //    Instantiate(moduleUI);
            //    moduleUI.transform.SetParent(parent);
            //}
            Transform canvas = GameObject.Find("Canvas").transform;
            for (int i = 0; i < canvas.childCount; i++)
            {
                if (canvas.GetChild(i).name == "ModuleUI")
                {
                    moduleUI = canvas.GetChild(i).gameObject;
                }
            }

            if (moduleUI.activeSelf)
                moduleUI.SetActive(false);
            else
                moduleUI.SetActive(true);
        }
    }

    //public void OnSelectLevel()
    //{
    //    try
    //    {
    //        selectedLevel = dropdown.value;
    //    }
    //    catch (System.Exception)
    //    {
    //        selectedLevel = 0;
    //        Debug.Log("GM: no dropdown in this scene");
    //        throw;
    //    }

    //}

    public static void OnTreeDigged(string digger)
    {
        if(digger == "Fox")
        {
            PC_tree++;
        }
        else
        {
            VR_tree++;
        }
        Debug.Log("Score: PC=" + PC_tree + ", VR=" + VR_tree);

        if(PC_tree >= end_point || VR_tree >= end_point)    // you win! end game
        {
            competitionFinish = true;
        }
    }

    public bool IsPcPlayer(int playerIndex)
    {
        return playerIndex == (int)LAYER.PC;
    }

    public static int GetWinner()
    {
        return (PC_tree > VR_tree) ? (int)LAYER.PC : (int)LAYER.VR;
    }

    public static void OnCollectStar()
    {
        if (++stars >= stars_num)
        {
            stars = 0;
            bMissionComlete = true;
            //SceneTransitor.LoadNewScene("SceneB_exploration");
        }
        GameObject.Find("Fox").GetComponent<AudioSource>().Play();
    }

    public void OnPassExQuestion()
    {
        if(++passedQuest >= pass_num)     // all pass! congratulation!
        {
            passedQuest = 0;
            bMissionComlete = true;
            //SceneTransitor.LoadNewScene("SceneB_competition");
        }
    }
}
