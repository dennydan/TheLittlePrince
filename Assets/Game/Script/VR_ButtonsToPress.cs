using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_ButtonsToPress : MonoBehaviour
{
    public bool staying = false;
    public Vector3 originPos_Btn, originPos_Obj;

    [SerializeField]
    public float movingSpeed = 0.01f;
    public float maxMoveDistance = 7f;
    public Transform obj;
    public GameObject restrictionArea;

    private void Start()
    {
        originPos_Btn = transform.parent.localPosition;
        originPos_Obj = obj.localPosition;
    }

    private void Update()
    {
        if (!staying)   //restore
        {
            //button restore, upper
            if(transform.parent.localPosition.y < originPos_Btn.y)
                transform.parent.localPosition = new Vector3(transform.parent.localPosition.x, transform.parent.localPosition.y + movingSpeed/4, transform.parent.localPosition.z);

            //obj restore
            if (obj.name == "wooden_door02")    //upper
            {
                if(obj.localPosition.y < originPos_Obj.y)
                    obj.localPosition = new Vector3(obj.localPosition.x, obj.localPosition.y + movingSpeed, obj.localPosition.z);
            }
            else if (obj.name == "house_lift_pad")  //lower
            {
                if (obj.localPosition.y > originPos_Obj.y)
                    obj.localPosition = new Vector3(obj.localPosition.x, obj.localPosition.y - movingSpeed*3, obj.localPosition.z);
            }
            else
            {
                Debug.Log("Wrong obj to restore");
            }
        }

        if(restrictionArea != null)
            restrictionArea.SetActive(!staying);  //open area when pushing
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            staying = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Platform"))    //pushed!!
        {
            staying = true;

            //button sink, lower
            if (transform.parent.localPosition.y >= originPos_Btn.y - maxMoveDistance/4)
                transform.parent.localPosition = new Vector3(transform.parent.localPosition.x, transform.parent.localPosition.y - movingSpeed/4, transform.parent.localPosition.z);

            //obj moving
            if (obj.name == "wooden_door02")    //lower
            {
                if (obj.localPosition.y >= originPos_Obj.y - maxMoveDistance)
                    obj.localPosition = new Vector3(obj.localPosition.x, obj.localPosition.y - movingSpeed, obj.localPosition.z);
            }
            else if (obj.name == "house_lift_pad")  //upper
            {
                if (obj.localPosition.y <= originPos_Obj.y + maxMoveDistance*3)
                    obj.localPosition = new Vector3(obj.localPosition.x, obj.localPosition.y + movingSpeed*3, obj.localPosition.z);
            }
            else
            {
                Debug.Log("Wrong obj to move");
            }
        }
    }
}
