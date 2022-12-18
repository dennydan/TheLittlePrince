using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_countDownSprite;

    [SerializeField]
    private GameObject m_startText;

    [SerializeField]
    private GameObject m_countDownText;

    public void Show()
    {
        StartCoroutine(StartCountDown(3));
    }

    IEnumerator StartCountDown(int time)
    {
        int second = time;
        m_countDownText.SetActive(true);
        Image countDownImg = m_countDownText.GetComponent<Image>();
        while (second > 0)
        {
            second--;
            if (second < m_countDownSprite.Length)
            {
                countDownImg.sprite = m_countDownSprite[second];
            }
            yield return new WaitForSeconds(1);
        }
        m_countDownText.SetActive(false);
        m_startText.SetActive(true);

        yield return new WaitForSeconds(1);
        m_startText.SetActive(false);
    }

}
