using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;
using System.Reflection;

public class VR_PlayerPointer : MonoBehaviour
{
    public ExploreUI m_explore;
    public SteamVR_Action_Boolean inputAction;
    private SteamVR_LaserPointer rightHandLaser;
    public Transform glowObject;
    public Material outlinedMaterial;
    public Material NormalMaterial;

    public int choosedOptionIndex;

    private void Start()
    {
        rightHandLaser = GameObject.Find("RightHand").GetComponent<SteamVR_LaserPointer>();
        rightHandLaser.PointerIn += PointerIn;
        rightHandLaser.PointerOut += PointerOut;
        rightHandLaser.PointerClick += PointerClick;
    }

    private void PointerClick(object sender, PointerEventArgs e)  // dig tree
    {
        MethodBase methodBase = MethodBase.GetCurrentMethod();
        if (e.target.name.Contains("Option0"))
        {
            choosedOptionIndex = (int)char.GetNumericValue(e.target.name.ToCharArray()[7]);
            Debug.Log("VR clicked: " + choosedOptionIndex);
            m_explore.SetOption(choosedOptionIndex - 1);
        }else if (e.target.name.Contains("ConfirmResult")){
            m_explore.ShowResultView(false);
            m_explore.ShowLeaveView();
        }else if(e.target.name.Contains("ConfirmLeave")){
            m_explore.Leave();
        }

        //if (e.target.name == "DevilTree(Clone)")
        //{
        //    if (e.target.GetChild(0).name == "final tree(Clone)")
        //    {
        //        // dig it!
        //        //m_Animator.SetTrigger(m_HashChop);
        //    }
        //}
    }

    private void PointerIn(object sender, PointerEventArgs e)  // glow tree
    {
        MethodBase methodBase = MethodBase.GetCurrentMethod();
        if(e.target.name.Contains("Option0"))
        {
            Debug.Log("VR choosed: " + (int)char.GetNumericValue(e.target.name.ToCharArray()[7]));
        }

        //if (e.target.name == "DevilTree(Clone)")
        //{
        //    //devilTree = e.target.GetComponent<DevilTreeHandler>();

        //    glowObject = e.target.GetChild(0);
        //    if (glowObject.name == "final tree(Clone)") // is final stage?
        //    {
        //        MeshRenderer glowChild = glowObject.GetComponentInChildren<MeshRenderer>();
        //        Material[] materials = glowChild.sharedMaterials;

        //        // change material to outline
        //        materials[0] = outlinedMaterial;
        //        glowChild.sharedMaterials = materials;
        //    }
        //}
    }

    private void PointerOut(object sender, PointerEventArgs e)   // de-glow tree
    {
        MethodBase methodBase = MethodBase.GetCurrentMethod();
        if (e.target.name.Contains("Option0"))
        {
            Debug.Log("VR left: " + choosedOptionIndex);
        }

        //if (e.target.name == "DevilTree(Clone)")
        //{
        //    Debug.Log("choosed");
        //    glowObject = e.target.GetChild(0);
        //    if (glowObject.name == "final tree(Clone)") // is final stage?
        //    {
        //        Debug.Log("leaved");
        //        MeshRenderer glowChild = glowObject.GetComponentInChildren<MeshRenderer>();
        //        Material[] materials = glowChild.sharedMaterials;

        //        // change material to outline
        //        materials[0] = NormalMaterial;
        //        glowChild.sharedMaterials = materials;
        //    }
        //}
    }
}
