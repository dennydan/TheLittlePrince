using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilTreeHandler : MonoBehaviour
{
    [SerializeField]
    public int HP = 5;
    public float timeValue = 5;
    public float timerReset = 5;
    public int growStage = 0;   // sampling, cone, tree
    public Object[] sapStage;
    public bool damagable = false;

    // Start is called before the first frame update
    void Start()
    {
        timeValue = timerReset;
    }

    // Update is called once per frame
    void Update()
    {
        //count down
        if (timeValue > 0 && growStage < 2)
        {
            timeValue -= Time.deltaTime;
            GrowUp();
        }
        else
            timeValue = 0;
    }

    private void GrowUp()
    {
        if (timeValue <= 0 && growStage < 2)
        {
            growStage++;
            timeValue = timerReset;

            ChangeOutfit(growStage);
        }
    }
    private void ChangeOutfit(int stage)
    {
        Transform child;
        GameObject go;
        Vector3 objectPos;

        if (stage == 1)
            child = this.transform.Find("little sapling");
        else
            child = this.transform.Find("glowing tree(Clone)");

        // Get child transform
        //objectPos = child.transform.localPosition;
        //objectRotate = child.transform.rotation;
        //objectScale = child.transform.localScale;

        if (stage == 1)  // fix generated position, position of prefab
        {
            objectPos = new Vector3(0, -0.175f ,0);
        }
        else
        {
            objectPos = new Vector3(2.12700009f, -0.1320000011f, 0);
        }

        Destroy(child.gameObject);

        go = Instantiate(sapStage[stage-1], this.transform.position, Quaternion.identity) as GameObject;
        go.transform.parent = this.transform;
        go.transform.localPosition = objectPos;
        //go.transform.rotation = objectRotate;
        //go.transform.localScale = objectScale;
        if (stage == 2)
            damagable = true;
    }

    public void OnDig(string digger)
    {
        if(damagable)
        {
            if (--HP == 0 && (this.gameObject != null))
            {
                GameManager.OnTreeDigged(digger);
                Destroy(this.gameObject);
            }
        }
    }
}
