using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitor : MonoBehaviour
{

    [SceneName]
    public string newSceneName;
    // Start is called before the first frame update
    void Start()
    {
 
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("SceneController_OnTriggerEnter");
        //SceneManager.LoadScene(0);
        if (other.GetComponent<CharacterController>().name == "Ellen")
        {
            SceneManager.LoadScene(newSceneName);
        }
        
    }
}
