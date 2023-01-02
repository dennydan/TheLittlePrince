using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField]
    public float timeRemaining;    //in seconds
    public bool triggered = false;

    [SerializeField]
    private GameObject m_rock;
    [SerializeField]
    private float m_rockFillAmount;

    [SerializeField]
    private Image m_timeLine;

    private float minutes, seconds, milliseconds, maxSeconds, remainSeconds;

    [SerializeField]
    TextMeshPro textMesh;

    private void Start()
    {
        //textMesh = GetComponentInChildren<TextMeshPro>();
        maxSeconds = timeRemaining;
        remainSeconds = timeRemaining;
        SetFillAmount(1.0f);
    }

    void Update()
    {
        if(triggered)   // gamer start!
        {
            // count down
            if (remainSeconds > 0)
            {
                remainSeconds -= Time.deltaTime;
                if(remainSeconds < 0)
                {
                    remainSeconds = 0;
                }

                //convert to mm , ss , mm
                minutes = Mathf.FloorToInt(remainSeconds / 60);
                seconds = Mathf.FloorToInt(remainSeconds % 60);
                milliseconds = Mathf.FloorToInt(remainSeconds * 100 % 60);

                //textMesh.text = minutes.ToString() + ":" + seconds.ToString() + "."+ milliseconds.ToString();
                textMesh.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
                SetFillAmount(remainSeconds/maxSeconds);
            }
            else
            {
                Debug.Log("Times UP!");
                triggered = false;
            }
        }
    }

    private void SetFillAmount(float fillAmount)
    {
        Debug.Log("SetFillAmount");
        Debug.Log(fillAmount);
        float rockFilAmount = (m_rockFillAmount) * fillAmount;
        m_rock.transform.localPosition = new Vector3(rockFilAmount, 0, 0);
        m_timeLine.fillAmount = fillAmount;

        Debug.Log(rockFilAmount);
    }
}
