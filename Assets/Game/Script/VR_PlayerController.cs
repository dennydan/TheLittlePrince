using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VR_PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 1;
    private CharacterController characterController;
    protected float m_ForwardSpeed;

    protected Animator m_Animator;

    private void Awake()
    {
        m_Animator = GameObject.Find("Prince").GetComponent<Animator>();
    }
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.axis.magnitude > 0.1f)
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
            //transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction,Vector3.up);

            Vector3 movement;
            movement = speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);

            m_ForwardSpeed = input.axis.normalized.magnitude * 8;
            // Set the animator parameter to control what animation is being played.
            m_Animator.SetFloat("ForwardSpeed", m_ForwardSpeed);

            Debug.Log("movement: " + movement);
            characterController.Move(movement - new Vector3(0, 9.81f, 0) * Time.deltaTime);
        }
    }
}
