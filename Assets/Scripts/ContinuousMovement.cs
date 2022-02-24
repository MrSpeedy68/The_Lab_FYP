using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    public float speed = 1f;
    public XRNode inputSource;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float additionalHeight = 0.2f;
    public bool isGrounded;
    public bool isOffGround = false;

    [SerializeField] private float fallingSpeed;
    private XROrigin rig;
    private Vector2 inputAxis;
    private CharacterController _character;

    void Start()
    {
        _character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis); //Get controller and the output joystick values
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0); //Get the rotation of the head
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y); //always move towards the direction of the head

        _character.Move(direction * Time.fixedDeltaTime * speed);

        //Gravity
        isGrounded = CheckIfGrounded();
        if(isGrounded)
        {
            fallingSpeed = 0;
        }
        else if (!isOffGround)
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
            _character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
        }
    }

    void CapsuleFollowHeadset()
    {
        _character.height = rig.CameraInOriginSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.Camera.transform.position);
        _character.center = new Vector3(capsuleCenter.x, _character.height / 2 + _character.skinWidth, capsuleCenter.z);
    }

    //A check to see if the player is on the ground
    bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(_character.center);
        float rayLength = _character.center.y + 0.01f;
        bool hashit = Physics.SphereCast(rayStart, _character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);

        return hashit;
    }

    public void ResetGravity()
    {
        fallingSpeed = 0;
    }
}
