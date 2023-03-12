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

    [SerializeField]
    private AudioClip[] m_tutorRecords;

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
    private AudioSource VRaudioSource;

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

        if (VRaudioSource == null)
        {
            // this can be added no matter VR enable or not.
            try
            {
                VRaudioSource = GameObject.Find("FollowHead").GetComponent<AudioSource>();
                Debug.Log("audio added.");
            }
            catch   // for SceneStart
            {
                VRaudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
                Debug.Log("Start audio added.");
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

        // Play Audio Record Clips
        //GetComponent<AudioSource>().Stop(); // cut previous clip if you click too fast.
        //FIXME: Volume is LOW when playing in unity.
        //I place audio source on canvas UI, a gameObject, or even camera itself won't fix it.
        //Both change the mp3 file and parameters in audio source didn't help.
        //I use a audio mixer to amplify the volume if we want a louder sound, but +20 dB will hear a little break voice, so set to +10 dB for now.
        if (bEnble)
        {
            if (SceneManager.GetActiveScene().name == "SceneB_cooperation")
            {
                Debug.Log("audio: SceneB_cooperation, idx: " + m_messageIndex);
                switch (m_messageIndex)
                {
                    case 1:
                        VRaudioSource.clip = m_tutorRecords[1];
                        VRaudioSource.Play();
                        StartCoroutine(waitForSound(m_tutorRecords[4])); // need to stuck here, but cannot use "while" or the game will crash
                        break;
                    case 2:
                        // do nothing
                        break;
                    case 3:
                        VRaudioSource.clip = m_tutorRecords[2];
                        VRaudioSource.Play();
                        break;
                    case 4:
                        VRaudioSource.clip = m_tutorRecords[3];
                        VRaudioSource.Play();
                        break;
                    default:
                        VRaudioSource.clip = m_tutorRecords[m_messageIndex];
                        VRaudioSource.Play();
                        break;
                }
            }
            else if (SceneManager.GetActiveScene().name == "SceneB_competition")
            {
                Debug.Log("audio: SceneB_competition, idx: " + m_messageIndex);
                switch (m_messageIndex)
                {
                    case 2:
                        VRaudioSource.clip = m_tutorRecords[2];
                        VRaudioSource.Play();
                        StartCoroutine(waitForSound(m_tutorRecords[6]));
                        break;
                    case 4: // choose m_tutorRecords[4/7/8] with amount 10/15/20
                        break;
                    case 5: // show winner badge
                        // do nothing
                        break;
                    case 6:
                        VRaudioSource.clip = m_tutorRecords[5];
                        VRaudioSource.Play();
                        break;
                    default:
                        VRaudioSource.clip = m_tutorRecords[m_messageIndex];
                        VRaudioSource.Play();
                        break;
                }
            }
            else
            {
                VRaudioSource.clip = m_tutorRecords[m_messageIndex];
                VRaudioSource.Play();
            }
        }
        else
        {
            VRaudioSource.Stop();
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

    IEnumerator waitForSound(AudioClip audioClip)
    {
        while(VRaudioSource.isPlaying)
        {
            yield return null;
        }

        // Audio has finished playing
        VRaudioSource.clip = audioClip;
        VRaudioSource.PlayDelayed(1f);  // delay 44100 samples as 1 second
    }
}
