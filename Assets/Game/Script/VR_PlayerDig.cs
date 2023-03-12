using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class VR_PlayerDig : MonoBehaviour
{
    public SteamVR_Action_Boolean inputAction;
    private SteamVR_LaserPointer rightHandLaser;
    protected Animator m_Animator;
    readonly int m_HashChop = Animator.StringToHash("Chop");
    protected DevilTreeHandler devilTree;
    public Transform glowObject;
    public Material outlinedMaterial;
    public Material NormalMaterial;
    public AudioSource chopSound;
    private float chopPitch;
    private float lowPitchRange = .95f;
    private float highPitchRange = 1.05f;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        rightHandLaser = GameObject.Find("RightHand").GetComponent<SteamVR_LaserPointer>();
        rightHandLaser.PointerIn += PointerIn;
        rightHandLaser.PointerOut += PointerOut;
        rightHandLaser.PointerClick += PointerClick;
    }

    private void PointerClick(object sender, PointerEventArgs e)  // dig tree
    {
        //Debug.Log("Click");
        if (e.target.name == "DevilTree(Clone)")
        {
            if (e.target.GetChild(0).name == "final tree(Clone)")
            {
                // dig it!
                m_Animator.SetTrigger(m_HashChop);
            }
        }
    }

    private void PointerIn(object sender, PointerEventArgs e)  // glow tree
    {
        //Debug.Log("Enter");
        if (e.target.name == "DevilTree(Clone)")
        {
            devilTree = e.target.GetComponent<DevilTreeHandler>();

            glowObject = e.target.GetChild(0);
            if (glowObject.name == "final tree(Clone)") // is final stage?
            {
                MeshRenderer glowChild = glowObject.GetComponentInChildren<MeshRenderer>();
                Material[] materials = glowChild.sharedMaterials;

                // change material to outline
                materials[0] = outlinedMaterial;
                glowChild.sharedMaterials = materials;
            }
        }
    }

    private void PointerOut(object sender, PointerEventArgs e)   // de-glow tree
    {
        //Debug.Log("Exit");
        if (e.target.name == "DevilTree(Clone)")
        {
            glowObject = e.target.GetChild(0);
            if (glowObject.name == "final tree(Clone)") // is final stage?
            {
                MeshRenderer glowChild = glowObject.GetComponentInChildren<MeshRenderer>();
                Material[] materials = glowChild.sharedMaterials;

                // change material to outline
                materials[0] = NormalMaterial;
                glowChild.sharedMaterials = materials;
            }

            devilTree = null;
        }
    }
    void DigTree()
    {
        chopSound.pitch = chopPitch * Random.Range(lowPitchRange, highPitchRange);
        chopSound.Play();
        devilTree.OnDig(this.name);
    }
}
