
using UnityEngine;
using UnityEngine.UI;

public class StatusWidget : MonoBehaviour
{
    [SerializeField] Text m_textMesh;
    // Start is called before the first frame update
    void Start()
    {
        m_textMesh.text = GameManager.version;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
