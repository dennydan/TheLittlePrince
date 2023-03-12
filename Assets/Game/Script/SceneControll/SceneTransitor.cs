using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitor : MonoBehaviour
{
    [SceneName]
    public string newSceneName;

    // need a instantiated object to add this, or you can't set audio clip by script.
    public AudioClip transAudioClips;
    private GameObject audioPlayerGameObject;

    private void OnTriggerEnter(Collider other)
    {
        print("SceneController_OnTriggerEnter");
        //SceneManager.LoadScene(0);
        if (other.GetComponent<CharacterController>().name == "Fox")
        {
            if(audioPlayerGameObject == null)
            {
                audioPlayerGameObject = new GameObject("AudioPlayerGameObject");
                audioPlayerGameObject.AddComponent<AudioSource>();
                audioPlayerGameObject.GetComponent<AudioSource>().clip = transAudioClips;
                DontDestroyOnLoad(audioPlayerGameObject);
            }

            audioPlayerGameObject.GetComponent<AudioSource>().Play();
            LoadNewScene(newSceneName);
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
