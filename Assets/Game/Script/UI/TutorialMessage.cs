using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.SpatialTracking;

public class TutorialMessage : MonoBehaviour
{
    [SerializeField]
    private GameObject m_leveTitle;

    [SerializeField]
    private GameObject m_leveTitle_VR;

    [SerializeField]
    private GameObject[] m_messageBoxs;

    [SerializeField]
    private GameObject[] m_messageBoxs_VR;

    [SerializeField]
    private GameObject m_countDown;

    private int m_messageIndex = 0;
    const int countDownTime = 3;
    const int waitingParam = 1;

    // VR角色
    private GameObject VROrigin;
    private Vector3 VRoriginPos;
    private Quaternion VRoriginRot;
    private Vector3 VRtutorialPos = new Vector3(5000f, 5000f, 500f);
    private Quaternion VRtutorialRot = new Quaternion(0, 1f, 0, 0);
    private bool VRenabled = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        if (m_leveTitle != null)
        {
            StartCountDown(() => {
                Time.timeScale = 0;
                m_leveTitle.SetActive(false);
                m_leveTitle_VR.SetActive(false);
                SetShowMessage(true);
            }, countDownTime);
        }
    }

    private void FixedUpdate()
    {
        if (!VRenabled)
        {
            // VR角色固定位置進行教學
            VROrigin = GameObject.Find("Player");
            if (VROrigin == null)
                Debug.Log("Not Found VR Device.");
            else
            {
                VRenabled = true;
                VRoriginPos = VROrigin.transform.position;
                VRoriginRot = VROrigin.transform.rotation;
                VROrigin.transform.position = VRtutorialPos;
                VROrigin.transform.rotation = VRtutorialRot;
                GameObject.Find("VRCamera").GetComponent<TrackedPoseDriver>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckMissionComplete())
        {
            AddMessageIndex();
            SetShowMessage(true);
            VRenabled = false;
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
        m_messageBoxs_VR[m_messageIndex].SetActive(bEnble);
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

        //復原VR位置
        VROrigin.transform.position = VRoriginPos;
        VROrigin.transform.rotation = VRoriginRot;
        GameObject.Find("VRCamera").GetComponent<TrackedPoseDriver>().enabled = true;
    }
}
