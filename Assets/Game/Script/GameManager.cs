using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Dropdown dropdown;
    public int selectedLevel;

    private void Awake()
    {
        if(Instance == null)
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

    public void OnSelectLevel()
    {
        try
        {
            selectedLevel = dropdown.value;
        }
        catch (System.Exception)
        {
            selectedLevel = 0;
            Debug.Log("GM: no dropdown in this scene");
            throw;
        }
            
    }
}
