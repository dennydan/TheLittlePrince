
public class StateMachine 
{
    private bool m_isEntering = false;
    private bool m_isTransit = false;
    private int m_currState = 0;
    private int m_nextState = 0;

    public int Tick()
    {
        if (m_isTransit)
        {
            m_currState = m_nextState;
            m_isTransit = false;
            m_isEntering = true;  
        }
        else
        {
            m_isEntering = false;
        }
        return m_currState;
    }

    public StateMachine(int initState)
    {
        m_currState = initState;
        m_nextState = initState;
    }

    public void NextState (int state)
    {
        this.m_nextState = state;
        this.m_isTransit = true;
    }

    public int Current()
    {
        return m_currState;
    }

    public bool IsEntering()
    {
        return m_isEntering;
    }
}
