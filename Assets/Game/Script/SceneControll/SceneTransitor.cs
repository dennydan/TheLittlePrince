using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitor : MonoBehaviour
{
    [SceneName]
    public string newSceneName;

    private void OnTriggerEnter(Collider other)
    {
        print("SceneController_OnTriggerEnter");
        //SceneManager.LoadScene(0);
        if (other.GetComponent<CharacterController>().name == "Fox")
        {
            SceneManager.LoadScene(newSceneName);
        }
    }

    //removable
    public static void LoadNewScene(string newScene)
    {
        Time.timeScale = 1;
        // reset score
        GameManager.PC_tree = 0;
        GameManager.VR_tree = 0;
        GameManager.stars = 0;
        GameManager.passedQuest = 0;
        SceneManager.LoadScene(newScene);
    }
}
