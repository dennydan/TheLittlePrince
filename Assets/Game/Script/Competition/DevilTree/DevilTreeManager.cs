
using UnityEngine;
using UnityEngine.UI;
using TMPro;



enum TREE_STATE  {
   Init = 0,
   Start,
   Spawn,
   Show_Winner,
   End ,
   Max,
}
public class DevilTreeManager : MonoBehaviour
{
    enum LAYER
    {
        PC = 7,
        VR = 8,
    }

    const string TREE_TAG = "Devil Trees";
    const int TREE_AMOUNT= 18;
    Transform[] m_treePositions = new Transform[TREE_AMOUNT];
    DevilTreeHandler[] m_devilTrees = new DevilTreeHandler[TREE_AMOUNT];

    [SerializeField]
    private DevilTreeHandler pf_devilTree;

    [SerializeField]
    private int m_spawnTime = 600;  // 毫秒

    [SerializeField]
    private string m_transitSceneName = "SceneB_cooperation";

    [SerializeField]
    private TutorialMessage m_tutorialMsg;

    [SerializeField]
    private Image m_tutorialImg;

    [SerializeField]
    private Image m_winnerImage;

    [SerializeField]
    private Sprite[] m_endPointSprites;

    [SerializeField]
    private Sprite[] m_winnerSprites;

    [SerializeField]
    private CountDown m_countDown;     // count down before game start
    [SerializeField]
    private CountDownTimer m_countDownTime;    // competition timer

    [SerializeField]
    private TextMeshPro m_foxTreeAmount;

    [SerializeField]
    private TextMeshPro m_princeTreeAmount;

    private int m_timer = 0;
    private StateMachine m_state = new StateMachine((int)TREE_STATE.Init);


    private void Awake()
    {
        //Debug.Log("DevilTreeManager");
        GameObject[] treeArray = GameObject.FindGameObjectsWithTag(TREE_TAG);
        
        for (int i = 0; i < treeArray.Length; ++i)
        {
            m_treePositions[i] = treeArray[i].transform;
        }
        m_state.NextState((int)TREE_STATE.Init);
    }

    private void Start()
    {
        m_state.NextState((int)TREE_STATE.Init);
    }

    void Update()
    {
        int currentState = m_state.Tick();
        UpdateDigTreeAmout();
        switch (currentState)
        {
            case (int)TREE_STATE.Init:
                {
                    if (m_state.IsEntering())
                    {
                        RandonEndPoint();
                        m_countDown.timeOutCallback = () => {
                            m_state.NextState(m_state.Current() + 1);
                        };
                    }
                    break;
                }
            case (int)TREE_STATE.Start:
                {
                    if (m_state.IsEntering())
                    {
                        //Debug.Log("TREE_STATE.Init");
                        m_timer = m_spawnTime;
                        m_countDownTime.triggered = true;
                        m_state.NextState(m_state.Current() + 1);
                    }
                    break;
                }
            case (int)TREE_STATE.Spawn:
                {
                    if(m_state.IsEntering())
                    {
                        //Debug.Log("TREE_STATE.Spawn");
                        spawnTree(Random.Range(0,TREE_AMOUNT-1));
                    }
                    else if(GameManager.competitionFinish)
                    {
                        // 條件完成，結束
                        m_state.NextState((int)TREE_STATE.Show_Winner);
                    }
                    else if(m_timer < 0)
                    {
                        // 冷卻時間到，重置後開始
                        m_state.NextState((int)TREE_STATE.Start);
                    }
                    else
                    {
                        m_timer--;
                    }
                    break;
                }
            case (int)TREE_STATE.Show_Winner:
                {
                    if (m_state.IsEntering())
                    {
                        Debug.Log("TREE_STATE.Show_Winner");
                        GameManager.competitionFinish = false;
                        Debug.Log("Score: PC=" + GameManager.PC_tree + ", VR=" + GameManager.VR_tree);

                        int winnerIndex = GameManager.GetWinner() == (int) LAYER.PC ? 0 : 1;
                        m_winnerImage.sprite = m_winnerSprites[winnerIndex];
                        GameManager.bMissionComlete = true;
                        m_timer = 420;
                        GameManager.PC_tree = GameManager.VR_tree = 0;
                    }
                    else if (m_timer < 0)
                    {
                        m_state.NextState((int)TREE_STATE.End);
                    }
                    else
                    {
                        m_timer--;
                    }
                }
                break;
            case (int)TREE_STATE.End:
                {
                    if(m_state.IsEntering())
                    {
                        Debug.Log("TREE_STATE.End");
                        m_tutorialMsg.Next();
                    }
                    break;
                }
            default:
                break;        
        
        }
    }

    void spawnTree(int num)
    {
        if (num < 0 || num >= TREE_AMOUNT )
        {
            //Debug.Log("CreateTree_OutOfRange...");
            return;
        }

        if (m_devilTrees[num] == null)
        {
            m_devilTrees[num] = Instantiate(pf_devilTree);
            m_devilTrees[num].transform.position = m_treePositions[num].position;
        }
        else
        {
            m_state.NextState((int)TREE_STATE.Spawn);
        }
            
    }

    private void RandonEndPoint()
    {
        Debug.Log("RandonEndPoint");
        int randPoint = Random.Range(0, 2);
        Debug.Log(randPoint);

        GameManager.end_point =  10 + 5 * randPoint;

        if ( randPoint < m_endPointSprites.Length)
        {
            m_tutorialImg.sprite = m_endPointSprites[randPoint];
        }
    }

    private void UpdateDigTreeAmout()
    {
        m_princeTreeAmount.text = GameManager.VR_tree.ToString();
        m_foxTreeAmount.text = GameManager.PC_tree.ToString();
    }
}
