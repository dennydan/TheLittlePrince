
using UnityEngine;



enum TREE_STATE  {
   Init = 0,
   Start,
   Spawn ,
   End ,
   Max,
}
public class DevilTreeManager : MonoBehaviour
{
    const string TREE_TAG = "Devil Trees";
    const int TREE_AMOUNT= 18;
    Transform[] m_treePositions = new Transform[TREE_AMOUNT];
    DevilTreeHandler[] m_devilTrees = new DevilTreeHandler[TREE_AMOUNT];

    [SerializeField]
    private DevilTreeHandler pf_devilTree;

    [SerializeField]
    private int m_spawnTime = 300;  // 毫秒

    [SerializeField]
    private string m_transitSceneName = "SceneB_cooperation";

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
        
    }

    void Update()
    {
        int currentState = m_state.Tick();        

        switch(currentState)
        {
            case (int)TREE_STATE.Init:
                {
                    m_state.NextState(m_state.Current() + 1);
                    break;
                }
            case (int)TREE_STATE.Start:
                {
                    if (m_state.IsEntering())
                    {
                        //Debug.Log("TREE_STATE.Init");
                        m_timer = m_spawnTime;
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
                        m_state.NextState((int)TREE_STATE.End);
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
            case (int)TREE_STATE.End:
                {
                    if(m_state.IsEntering())
                    {
                        //SceneTransitor.LoadNewScene(m_transitSceneName);
                        //Debug.Log("TREE_STATE.End");
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
    private void ResetData()
    {}
}
