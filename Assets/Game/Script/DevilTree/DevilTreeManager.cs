
using UnityEngine;

public class DevilTreeManager : MonoBehaviour
{
    const int TREE_AMOUNT= 18;
    Transform[] m_treePositions = new Transform[TREE_AMOUNT];
    DevilTreeHandler[] m_devilTrees;

    private void Awake()
    {
        for(int i = 0; i < TREE_AMOUNT; ++i)
        {
            m_treePositions[i] = GameObject.Find("pos_0" + i.ToString()).transform;
        } 
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
