using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

struct Question {
    public GameObject titleObject;
    public bool isPicked;
    public bool bfinish;
    public int index;
    public int[] answers;
    public AudioClip qClip;
}

enum PickQuestion_State { 
    Init = 0,
    Start,
    PickOption,
    CheckOption,
    FadeOut,
    ShowPuzzle,
    AddPuzzle,
    LeavePuzzle,
    End,
    Leave,
}

public class ExploreUI : MonoBehaviour
{
    const int Question_Amount = 10;
    const int PUZZLE_MAX = 12;
    // Start is called before the first frame update
    [SerializeField] GameObject[] m_titles;
    [SerializeField] GameObject[] m_amoutImg;
    [SerializeField] GameObject[] m_puzzles;
    [SerializeField] GameObject[] m_correctHints;
    [SerializeField] GameObject[] m_wrongHints;
    [SerializeField] GameObject[] m_optionSpawner;
    [SerializeField] ExploreAnswerPool m_answerPool;
    [SerializeField] GameObject m_exploreNode;
    [SerializeField] GameObject[] m_resultView;  // PC And VR
    [SerializeField] GameObject[] m_leaveView;   // PC And VR
    [SerializeField] AudioClip[] m_correctOrWrongClip;  // 0 for correct, 1 for wrong
    [SerializeField] AudioClip[] m_questionClips;

    Question[] m_questions = new Question[Question_Amount];

    private StateMachine m_state = new StateMachine((int)TREE_STATE.Init);
    private int m_questionIndex = 0;
    private int m_optionIndex = -1;
    private List<int> m_optionArray = new List<int>();
    private int[,] m_answerArray = { {0,2,4},{0,3,4},{1,2,4},{0,1,2},{1,2,3},
                                 {0,1,2},{1,2,3},{1,3,4},{1,2,4},{0,1,3}};

    private List<int> m_pickedPool = new List<int>();

    private int m_puzzleAmount = 0;
    private int m_timer = 0;
    private bool m_bShowPuzzle = false;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        // ªì©l¤Æ
        m_puzzleAmount = 0;
        for (int i = 0; i < m_titles.Length; ++i)
        {
            m_questions[i].index = i;
            m_questions[i].isPicked = false;
            m_questions[i].bfinish = false;
            m_questions[i].titleObject = m_titles[i];
            m_questions[i].answers = new int[3];
            for(int j = 0; j < m_answerArray.GetLength(1); ++j)
            {
                m_questions[i].answers[j] = m_answerArray[i, j];
                Debug.Log("AnswerArray:" + " i " + i + " j " + j + m_answerArray[i,j]);
            }
            m_questions[i].qClip = m_questionClips[i];
        }

        for(int i = 0; i < m_amoutImg.Length; ++i)
        {
            m_amoutImg[i].SetActive(false);
        }

        for (int i = 1; i < m_puzzles.Length; ++i)
        {
            m_puzzles[i].SetActive(false);
        }

        ResetHint();
        m_answerPool.InitAnswer(m_answerArray);
        m_state.NextState((int)TREE_STATE.Init);

        m_exploreNode.transform.localScale = new Vector3(0, 0, 0);

        gameObject.transform.parent.GetComponentInChildren<CountDown>().timeOutCallback = ()=>
        {
            StartQuestion();
        };
        ShowResultView(false);
        ShowLeaveView(false);
    }

    // Update is called once per frame
    void Update()
    {
        int currentState = m_state.Tick();
        switch (currentState)
        {
            case (int)PickQuestion_State.Init:
                {
                    if (m_state.IsEntering())
                    {
                        Debug.Log("PickQuestion_State.Init");
  
                    }
                }
                break;
            case (int)PickQuestion_State.Start:
                {
                    if (m_state.IsEntering())
                    {
                        Debug.Log("PickQuestion_State.Start");
                        m_exploreNode.transform.localScale = new Vector3(1, 1, 1);
                        m_optionArray.Clear();
                        m_pickedPool.Clear();
                        CloseAllTitles();

                        if (PickQuestion(Random.Range(0, Question_Amount - 1)))
                        {
                            m_answerPool.CreateOptionObject(m_questionIndex);
                            ResetHint();
                            m_state.NextState(m_state.Current() + 1);
                        }
                        else
                        {
                            m_state.NextState(m_state.Current());
                        }
                    }
                }
                break;
            case (int)PickQuestion_State.PickOption:
                {
                    if (m_state.IsEntering())
                    {
                        Debug.Log("PickQuestion_State.PickOption");
                        //m_state.NextState((int)PickQuestion_State.End);
                    }
                }
                break;

            case (int)PickQuestion_State.CheckOption:
                {
                    if (m_state.IsEntering())
                    {
                        Debug.Log("PickQuestion_State.CheckOption");
                        
                        if (CheckAnswer(m_questionIndex))
                        {
                            for (int i = 0; i < m_optionArray.Count; ++i)
                            {
                                Debug.Log("m_optionArray[i]:"+ m_optionArray[i]);

                                if (m_optionIndex == m_optionArray[i])
                                {
                                    if (CheckPickedPool(m_optionIndex))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        m_pickedPool.Add(m_optionIndex);
                                        m_optionArray.Add(m_optionIndex);
                                        m_bShowPuzzle = true;
                                        AddPuzzleAmout();
                                    }
                                }
                            }
                            // Right
                            m_correctHints[m_optionIndex].SetActive(true);
                            GetComponent<AudioSource>().PlayOneShot(m_correctOrWrongClip[0]);
                        }
                        else
                        {
                            // Wrong
                            m_wrongHints[m_optionIndex].SetActive(true);
                            GetComponent<AudioSource>().PlayOneShot(m_correctOrWrongClip[1]);
                        }
                        m_timer = 250;
                    }else if (m_timer < 0)
                    { 
                        m_state.NextState(m_state.Current() + 1);
                    }
                    else
                    {
                        m_timer--;
                    }
                }
                break;
            case (int)PickQuestion_State.FadeOut:
                {
                    if (m_state.IsEntering())
                    {
                        Debug.Log("PickQuestion_State.FadeOut");
                        m_wrongHints[m_optionIndex].SetActive(false);
                        m_correctHints[m_optionIndex].SetActive(false);
                        int state = m_bShowPuzzle ? (int)PickQuestion_State.ShowPuzzle : (int)PickQuestion_State.PickOption;
                        m_bShowPuzzle = false;
                        m_answerPool.ShowPuzzleHint(0, false);  // Close All Hint
                        m_state.NextState(state);
                    }
                }
                break;
            case (int)PickQuestion_State.ShowPuzzle:
                {
                    if (m_state.IsEntering())
                    {
                        Debug.Log("PickQuestion_State.ShowPuzzle");
                        // Show Puzzle Map
                        m_timer = 10;
                    }
                    else if (m_timer < 0)
                    {
                        m_state.NextState(m_state.Current() + 1);
                    }
                    else
                    {
                        m_timer--;
                    }
                }
                break;
            case (int)PickQuestion_State.AddPuzzle:
                {
                    if (m_state.IsEntering())
                    {
                        Debug.Log("PickQuestion_State.AddPuzzle");
                        // Random Puzzle
                        int puzzleIndex = Random.Range(1, PUZZLE_MAX);
                        if (m_puzzles[puzzleIndex].activeSelf)
                        {
                            for (int i = 1; i < m_puzzles.Length; ++i)
                            {
                                if(!m_puzzles[i].activeSelf)
                                {
                                    m_puzzles[i].SetActive(true);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            m_puzzles[puzzleIndex].SetActive(true);
                        }
                        
                    }
                    else if (m_timer < 0)
                    {
                        m_state.NextState(m_state.Current() + 1);
                    }
                    else
                    {
                        m_timer--;
                    }
                }
                break;
            case (int)PickQuestion_State.LeavePuzzle:
                {
                    if (m_state.IsEntering())
                    {
                        Debug.Log("PickQuestion_State.LeavePuzzle");

                        
                        m_timer = 100;
                    }
                    else if(m_timer < 0)
                    {
                        int state = IsOptionFinish() ? (int)PickQuestion_State.Start : (int)PickQuestion_State.PickOption;
                        state = IsPuzzleDone() ? (int)PickQuestion_State.End : state;
                        m_state.NextState(state);
                    }
                    else
                    {
                        m_timer--;
                    }
                }
                break;
            case (int)PickQuestion_State.End:
                {
                    if (m_state.IsEntering())
                    {
                        Debug.Log("PickQuestion_State.End");
                        CloseAllTitles();
                        m_puzzles[0].SetActive(false);
                        for (int i = 0; i < m_optionSpawner.Length; ++i)
                        {
                            m_optionSpawner[i].SetActive(false);
                        }

                        for (int i = 0; i < m_amoutImg.Length; ++i)
                        {
                            m_amoutImg[i].SetActive(false);
                        }

                        ShowResultView();
                    }
                }
                break;
            case (int)PickQuestion_State.Leave:
                {
                    if (m_state.IsEntering())
                    {
                        Debug.Log("PickQuestion_State.Leave");
                        ShowLeaveView();
                        m_timer = 600;
                    }
                    else if (m_timer < 0)
                    {
                        Leave();
                    }
                    else
                    {
                        m_timer--;
                    }
                }
                break;
            default:
                break;
        }
    }

    private void ResetHint()
    {
        for (int i = 0; i < m_correctHints.Length; ++i)
        {
            m_wrongHints[i].SetActive(false);
            m_correctHints[i].SetActive(false);
        }
    }

    public void StartQuestion()
    {
        m_state.NextState((int)PickQuestion_State.Start);
    }

    private bool PickQuestion(int index)
    {
        if (m_questions[index].isPicked)
        {
            return false;
        }

        GameObject.Find("FollowHead").GetComponent<AudioSource>().clip = m_questions[index].qClip;
        GameObject.Find("FollowHead").GetComponent<AudioSource>().Play();
        m_questions[index].isPicked = true;
        m_questions[index].titleObject.SetActive(true);
        m_questionIndex = index;
        return true;
    }

    private void CloseAllTitles()
    {
        foreach(Question obj in m_questions)
        {
            obj.titleObject.SetActive(false);
        }
    }

    public void SetOption(int index)
    {
        if (m_state.Current() != (int)PickQuestion_State.PickOption) return;

        Debug.Log("SetOption:" + index.ToString());
        m_optionIndex = index;
        
        m_state.NextState(m_state.Current() + 1);
    }

    private bool CheckAnswer(int questionIndex)
    {
        int[] ans = m_questions[questionIndex].answers;
        for (int i = 0; i < ans.Length; ++i)
        {
            if (ans[i] == m_optionIndex)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsOptionFinish()
    {
        int count = m_optionArray.Count;

        if (count >= 3)
        {
            return true;
        }
        else
        {
            int[] ans = m_questions[m_optionIndex].answers;
            for (int i = 0; i < ans.Length; ++i)
            {
                if (ans[i] == -1)
                {
                    count++;
                }
            }
        }

        return count >= 3;
    }

    private bool IsPuzzleDone()
    {
        return m_puzzleAmount >= PUZZLE_MAX;
    }

    private void AddPuzzleAmout()
    {
        if (m_puzzleAmount > 0)
        {
            m_amoutImg[m_puzzleAmount - 1].SetActive(false);
        }
        m_amoutImg[m_puzzleAmount].SetActive(true);
        m_answerPool.ShowPuzzleHint(m_puzzleAmount);
        m_puzzleAmount++;
    }

    public void ShowResultView(bool bActive = true)
    {
        for (int i = 0; i < m_resultView.Length; ++i)
        {
            m_resultView[i].SetActive(bActive);
        }
    }

    public void ShowLeaveView(bool bActive = true)
    {
        for (int i = 0; i < m_leaveView.Length; ++i)
        {
            m_leaveView[i].SetActive(bActive);
        }
    }

    public void Leave()
    {
        SceneManager.LoadScene("SceneStart");
    }

    private bool CheckPickedPool(int answer)
    {
        for(int i = 0; i < m_pickedPool.Count; ++i)
        {
            if(answer == m_pickedPool[i])
            {
                return true;
            }
        }
        return false;
    }
}
