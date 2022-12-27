using UnityEngine;

[System.Serializable]

public class MapTransforms
{
    public Transform vrTarget;
    public Transform ikTarget;

    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void VRMapping()
    {
        ikTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        ikTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class AvatarController : MonoBehaviour
{
    [SerializeField] public MapTransforms head;
    [SerializeField] public MapTransforms lefthand;
    [SerializeField] public MapTransforms righthand;

    [SerializeField] private float turnSmoothness;
    [SerializeField] Transform ikHead;
    [SerializeField] Vector3 headBodyOffset;

    private void Start()
    {
        AvatarController VR_avatar = GameObject.Find("Prince").GetComponent<AvatarController>();

        VR_avatar.head.vrTarget = GameObject.Find("VRCamera").transform;
        VR_avatar.lefthand.vrTarget = GameObject.Find("LeftHand").transform;
        VR_avatar.righthand.vrTarget = GameObject.Find("RightHand").transform;
    }

    private void LateUpdate()
    {
        transform.position = ikHead.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(ikHead.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        head.VRMapping();
        lefthand.VRMapping();
        righthand.VRMapping();
    }
}
