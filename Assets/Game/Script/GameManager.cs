using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //public TMP_Dropdown dropdown;
    //public int selectedLevel;

    GameObject moduleUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }else if(Instance != this)
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
                if(canvas.GetChild(i).name == "ModuleUI")
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

    GameObject FindInActiveObjectByName(string name)
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
