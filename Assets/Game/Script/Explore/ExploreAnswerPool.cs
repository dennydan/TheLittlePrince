using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Explore_Option  {
    Hope = 0,
    Remind,
    Authority,
    Memory,
    Freedom,
    Love,
    Friendship,
    Encourage,
    Curiousity,
    Guide,
    Wealth,
}
public class ExploreAnswerPool : MonoBehaviour
{
    [SerializeField] GameObject[] pf_answers;
    [SerializeField] GameObject[] m_optionNode;

    private int[,] m_answerArray = new int[3,10];
    private int[] m_questionInterval = { 4, 4, 4, 5, 5, 4, 4, 4, 3, 4, 4 };
    
     
    public void InitAnswer(int[,] ansArray)
    {
        m_answerArray = ansArray;
    }

    // calcuate index of options
    private GameObject[] GetRandomAnswer(int questionIndex, int maxNum)
    {
        int maxOption = maxNum;
        GameObject[] options = new GameObject[maxOption];
        int interval = m_questionInterval[questionIndex];

        int startIndex = interval > maxOption ?
            Random.Range(0, interval - maxOption) : 0;
        for(int i = 0; i < questionIndex; ++i )
        {
            startIndex = startIndex + m_questionInterval[i];
        }

        for(int i = 0; i < maxOption; ++i)
        {
            options[i] = Instantiate(pf_answers[startIndex + i]);
        }
        
        return options;
    }

    // Create answer implement  
    public void CreateOptionObject(int questionIndex)
    {
        GameObject[] correctOptions = GetRandomAnswer(questionIndex, 3);
        GameObject[] wrongOptions = GetRandomAnswer(Random.Range(questionIndex+1, (int)Explore_Option.Wealth), 2);

        int correctIndex = 0;
        int wrongIndex = 0;
        for(int i = 0; i < 5; ++i)
        {
            // Remove all children from parent
            for(int childIndex = 0; childIndex < m_optionNode[i].transform.childCount; ++childIndex)
            {
                Destroy(m_optionNode[i].transform.GetChild(childIndex).gameObject);
            }

            bool hasOption = false;
            for (int j = 0; j < m_answerArray.GetLength(1); ++j) // 3
            {
                if (m_answerArray[questionIndex, j] == i)
                {
                    correctOptions[correctIndex].transform.parent = m_optionNode[i].transform;
                    correctOptions[correctIndex].transform.position = m_optionNode[i].transform.position;
                    correctIndex++;
                    hasOption = true;
                    break;
                }
            }
            if (!hasOption && wrongIndex < wrongOptions.Length)
            {
                wrongOptions[wrongIndex].transform.parent = m_optionNode[i].transform;
                wrongOptions[wrongIndex].transform.position = m_optionNode[i].transform.position;
                wrongIndex++;
            }
        }
    }
}
