using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDig : MonoBehaviour
{
    public ChoosingFromRaycast choosed;
    protected Animator m_Animator;
    readonly int m_HashChop = Animator.StringToHash("Chop");
    protected DevilTreeHandler devilTree;
    public AudioSource chopSound;
    private float chopPitch;
    private float lowPitchRange = .95f;
    private float highPitchRange = 1.05f;

    private void Start()
    {
        choosed = GetComponent<ChoosingFromRaycast>();
        chopPitch = chopSound.pitch;
    }

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        //if (Input.GetButtonDown("XRI_Right_TriggerButton") || Input.GetButtonDown("XRI_Right_GripButton"))
        if (Input.GetButtonDown("Fire1"))
        {
            if (choosed.choosedObject != null)
            {
                if (choosed.choosedObject.CompareTag("Devil Trees"))
                {
                    devilTree = choosed.choosedObject.GetComponent<DevilTreeHandler>();
                    if (devilTree.transform.GetChild(0).name == "final tree(Clone)")
                    {
                        // dig it!
                        m_Animator.SetTrigger(m_HashChop);
                    }
                }
            }
        }
    }

    void DigTree()
    {
        chopSound.pitch = chopPitch * Random.Range(lowPitchRange,highPitchRange);
        chopSound.Play();
        devilTree.OnDig(this.name);
    }
}
