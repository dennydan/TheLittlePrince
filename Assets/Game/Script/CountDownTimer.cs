using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField]
    public float timeRemaining;    //in seconds
    public bool triggered = false;
    private float minutes, seconds, milliseconds;

    TextMeshPro textMesh;

    private void Start()
    {
        textMesh = GetComponentInChildren<TextMeshPro>();
    }

    void Update()
    {
        if(triggered)   // gamer start!
        {
            // count down
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                if(timeRemaining < 0)
                {
                    timeRemaining = 0;
                }

                //convert to mm , ss , mm
                minutes = Mathf.FloorToInt(timeRemaining / 60);
                seconds = Mathf.FloorToInt(timeRemaining % 60);
                milliseconds = Mathf.FloorToInt(timeRemaining * 100 % 60);

                //textMesh.text = minutes.ToString() + ":" + seconds.ToString() + "."+ milliseconds.ToString();
                textMesh.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
            }
            else
            {
                Debug.Log("Times UP!");
                triggered = false;
            }
        }
    }
}
