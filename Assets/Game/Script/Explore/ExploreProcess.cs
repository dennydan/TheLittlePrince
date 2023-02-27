using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EXPLORE_STATE {
    Init = 0,
    PICK_QUESTION,
    START,
    SHOW_PUZZLE,
    End,
}

public class ExploreProcess : MonoBehaviour
{
    [SerializeField] 

    private StateMachine m_state = new StateMachine((int)EXPLORE_STATE.Init);
    const int PUZZLE_MAX = 12;
    private int[,] Questions = { {1,3,5},{1,4,5},{2,3,5},{1,2,3},{2,3,4},
                                 {1,2,3},{2,3,4},{2,4,5},{3,5,-1},{1,2,4}};

    private ArrayList m_questionsOption = new ArrayList();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int currentState = m_state.Tick();

        switch (currentState)
        {
            case (int)EXPLORE_STATE.Init:
                {
                    if (m_state.IsEntering())
                    {
                        int[] a = { 1, 2 };
                        for(int i = 0; i < Questions.GetLength(0); ++i)
                        {
                            int[] options = (int[])Questions.GetValue(i);
                            m_questionsOption.Add(options);
                            //Debug.Log("Questions", options.ToString());
                        }
                        m_state.NextState((int)EXPLORE_STATE.PICK_QUESTION);
                    }
                }
                break;
            case (int)EXPLORE_STATE.PICK_QUESTION:
                {
                    if (m_state.IsEntering())
                    {
                        PickQuestion();
                        m_state.NextState((int)EXPLORE_STATE.START);
                    }
                }
                break;
            case (int)EXPLORE_STATE.START:
                {

                }
                break;
            case (int)EXPLORE_STATE.End:
                {

                }
                break;
            default:
                break;
        }

    }

    int PickQuestion()
    {
        int removeIndex = Random.Range(0, m_questionsOption.Count);
        m_questionsOption.RemoveAt(removeIndex);
        return removeIndex;
    }

}
