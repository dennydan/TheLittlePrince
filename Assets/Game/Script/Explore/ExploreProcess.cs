using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EXPLORE_STATE {
    Init = 0,
    Start,
    SHOW_PUZZLE,
    End,
}


public class ExploreProcess : MonoBehaviour
{
    private StateMachine m_state = new StateMachine((int)EXPLORE_STATE.Init);
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

                }
                break;
            case (int)EXPLORE_STATE.Start:
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
}
