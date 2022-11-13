using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosingFromRaycast : MonoBehaviour
{
    public float range = 5;
    public Transform choosedObject;
    public bool debugRay = true;
    private Transform glowObject;
    public Material outlinedMaterial;
    private MeshRenderer previousChild;
    public Material[] NormalMaterials;
    public string previousName = null;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.forward;
        Vector3 pos = transform.position + new Vector3(0,1,0); // raise ray position, it's too low
        Ray theRay = new Ray(pos, transform.TransformDirection(direction * range));
        if(debugRay)
            Debug.DrawRay(pos, transform.TransformDirection(direction * range));

        if(Physics.Raycast(theRay, out RaycastHit hit, range))
        {
            if (hit.collider.tag != "Devil Trees")  // not tree
            {
                if (debugRay)
                    Debug.Log("Tree is a lie!");
                choosedObject = null;
                previousName = null;
                glowObject = null;

                // put origin materials back
                if (previousChild != null)
                    previousChild.sharedMaterials = NormalMaterials;
                // clear temp
                previousChild = null;
                NormalMaterials = null;
            }
            else // is tree
            {
                if (previousName == hit.collider.name) // same tree
                {
                    var tmp = hit.collider.transform.GetChild(0);
                    if (tmp.name != "final tree(Clone)")
                    {
                        if (debugRay)
                            Debug.Log("Not Even Close!!");
                        // clear tmp
                        choosedObject = null;
                        previousName = null;
                        glowObject = null;
                    }
                    else
                    {
                        if (debugRay)
                            Debug.Log("Do Nothing!");
                    }
                }
                else // not the same tree!!
                {
                    // put origin materials back
                    if (previousChild != null)
                        previousChild.sharedMaterials = NormalMaterials;
                    // clear temp
                    previousChild = null;
                    NormalMaterials = null;
                    glowObject = null;


                    if (debugRay)
                        Debug.Log("Choosed!");
                    choosedObject = hit.collider.transform;
                    previousName = choosedObject.name;
                    glowObject = choosedObject.GetChild(0);
                    if (glowObject.name == "final tree(Clone)") // is final stage?
                    {
                        MeshRenderer glowChild = glowObject.GetComponentInChildren<MeshRenderer>();
                        Material[] materials = glowChild.sharedMaterials;
                        // save current choosing, recover when not choosed
                        previousChild = glowObject.GetComponentInChildren<MeshRenderer>();
                        NormalMaterials = glowChild.sharedMaterials;

                        // change material to outline
                        materials[0] = outlinedMaterial;
                        glowChild.sharedMaterials = materials;
                    }
                }
            }
        }
        else
        {
            if (debugRay)
                Debug.Log("Empty!");
            // put origin materials back
            if (previousChild != null)
                previousChild.sharedMaterials = NormalMaterials;
            // clear tmp
            choosedObject = null;
            previousName = null;
            glowObject = null;
        }
    }
}
