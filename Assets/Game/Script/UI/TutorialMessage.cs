using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class TutorialMessage : MonoBehaviour
{
    [SerializeField]
    private GameObject m_leveTitle;

    [SerializeField]
    private GameObject[] m_messageBoxs;

    [SerializeField]
    private GameObject m_countDown;

    private int m_messageIndex = 0;
    const int countDownTime = 3;
    const int waitingParam = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (m_leveTitle != null)
        {
            StartCountDown(() => {
                Time.timeScale = 0;
                m_leveTitle.SetActive(false);
                SetShowMessage(true);
            }, countDownTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckMissionComplete())
        {
            AddMessageIndex();
            SetShowMessage(true);
        }
    }

    private void StartCountDown(System.Action callback, int time)
    {
        
        StartCoroutine(CountDown(callback, time));
    }

    IEnumerator CountDown(System.Action callback, int time)
    {
        int second = time;
        while (second > 0)
        {
            yield return new WaitForSeconds(waitingParam);
            second--;
        }
        yield return new WaitForSeconds(waitingParam);
        callback();
    }

    public void SetShowMessage(bool bEnble)
    {
        if (m_messageIndex >= m_messageBoxs.Length || m_messageIndex < 0)
        {
            print("Out_Of_Range[Next()]");
            return;
        }
        m_messageBoxs[m_messageIndex].SetActive(bEnble);
    }


    public void Next()
    {
        Debug.Log("Next");
        SetShowMessage(false);
        AddMessageIndex();
        SetShowMessage(true);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    private void AddMessageIndex()
    {
        m_messageIndex++;
    }

    private bool CheckMissionComplete()
    {
        bool bComplete = GameManager.bMissionComlete;
        GameManager.bMissionComlete = false;
        return bComplete;
    }

    public void CountDownAndStart()
    {
        Time.timeScale = 1;
        if (m_countDown == null) return;
        SetShowMessage(false);
        m_countDown.GetComponent<CountDown>().Show();
    }
}
