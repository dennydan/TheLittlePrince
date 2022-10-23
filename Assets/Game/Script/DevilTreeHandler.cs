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
    //public Mesh[] treeMesh;
    //public Mesh[] soilMesh;
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
        Vector3 objectScale, objectPos;
        //GameObject gameObject1 = GameObject.Find("Sapling");
        //MeshFilter meshFilter1 = gameObject1.GetComponent<MeshFilter>();
        //meshFilter1.mesh = treeMesh[stage-1];

        //GameObject gameObject2 = GameObject.Find("Soil");
        //MeshFilter meshFilter2 = gameObject2.GetComponent<MeshFilter>();
        //meshFilter2.mesh = soilMesh[stage - 1];

        if(stage == 1)
            child = this.transform.Find("little sapling1");
        else
            child = this.transform.Find("glowing tree1(Clone)");

        // Get child transform
        objectScale = child.transform.localScale;
        objectPos = child.transform.position;

        if(stage == 1)  // fix generated position
        {
            objectPos = new Vector3(objectPos.x - 0.712000012f, objectPos.y - 0.63499999f, objectPos.z - 0.194999993f);
        }
        else
        {
            objectPos = new Vector3(objectPos.x - 0.15f, objectPos.y, objectPos.z + 0.1f);
            //objectScale = new Vector3(objectScale.x / 1.5f, objectScale.y / 1.5f, objectScale.z / 1.5f);
        }

        Destroy(child.gameObject);

        go = Instantiate(sapStage[stage-1], this.transform.position, Quaternion.identity) as GameObject;
        go.transform.parent = this.transform;
        go.transform.localScale = objectScale;
        go.transform.position = objectPos;
        if (stage == 2)
            damagable = true;
    }

    public void OnDig()
    {
        if(damagable)
        {
            if (--HP == 0 && (this.gameObject != null))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
