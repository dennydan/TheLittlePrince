using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitor : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("SceneController_OnTriggerEnter");
        //SceneManager.LoadScene(0);
        if (other.GetComponent<CharacterController>())
        {
            SceneManager.LoadScene("SceneB_competition");
        }
        
    }
}
