using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VR_PlayerController : MonoBehaviour
{
    //public SteamVR_Action_Vector2 input;
    public SteamVR_Action_Boolean inputUp, inputDown, inputLeft, inputRight;
    private Vector3 inputXPosition, inputYPosition;
    public float speed = 1;
    private CharacterController characterController;
    protected float m_ForwardSpeed;

    public Animator m_Animator;
    public Vector3 movement;
    public Vector3 direction;
    public bool debuglog;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        m_Animator = GameObject.Find("Prince").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input axis
        if (debuglog)
            Debug.Log("input: " + inputUp.state + ", " + inputDown.state + ", " + inputLeft.state + ", " + inputRight.state);

        if (inputUp.state)
            inputYPosition = new Vector3(0, 0, 1f);
        else if (inputDown.state)
            inputYPosition = new Vector3(0, 0, -1f);
        else
            inputYPosition = new Vector3(0, 0, 0);
        if (inputRight.state)
            inputXPosition = new Vector3(1f, 0, 0);
        else if (inputLeft.state)
            inputXPosition = new Vector3(-1f, 0, 0);
        else
            inputXPosition = new Vector3(0, 0, 0);

        if (debuglog)
            Debug.Log("move: " + inputXPosition + ", " + inputYPosition);

        // Set the animator parameter to control what animation is being played.
        //m_ForwardSpeed = input.axis.magnitude * 8;
        m_ForwardSpeed = (inputXPosition.magnitude + inputYPosition.magnitude) * 8;
        if (debuglog)
            Debug.Log("m_ForwardSpeed: " + m_ForwardSpeed);
        m_Animator.SetFloat("ForwardSpeed", m_ForwardSpeed);

        //if (input.axis.magnitude > 0.1f)
        if (m_ForwardSpeed > 0.1f)
        {
            //direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
            ////transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction,Vector3.up);
            
            // inputY using z, stands for Y axis
            direction = Player.instance.hmdTransform.TransformDirection(new Vector3(inputXPosition.x, 0, inputYPosition.z));
            direction = Vector3.ProjectOnPlane(direction, Vector3.up);
            direction.Normalize();
            if (debuglog)
                Debug.Log("direction: " + direction);

            movement = speed * Time.deltaTime * direction;
            if (debuglog)
                Debug.Log("movement: " + movement);
            characterController.Move(movement - new Vector3(0, 9.81f, 0) * Time.deltaTime);
        }
    }
}
