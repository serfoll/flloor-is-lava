using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{

    [SerializeField]
    public float speed = 1f;
    public float gravity = -9.81f;
    public float additionalHeight;
    public LayerMask groundLayer;

    public bool jumpBtn { get; set; }
    public Vector2 inputAxis { get; set; }
    public bool canMove;

    [SerializeField]
    private XRNode leftInputSource;
    [SerializeField]
    private XRNode rightInputSource;
    [SerializeField]
    float jumpForce;

    private CharacterController character;
    private XRRig rig;
    private Vector3 fallingSpeed;

    int jumpCounter;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();

        jumpCounter = 0;
        canMove = true;
    }

    private void FixedUpdate()
    {

        if (canMove)
        {
            CapsuleFollowHeadset();

            //leftDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
            //Define variable headYaw of type Quaternion, set the value to rotataion value of the XRRig camera on the y Axis
            Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);

            //Define variable direction of type vector3, set it to a new vecotor 3 based on the x and y of the inputAxis variable
            //Multiply it with headYaw to move in the direction the player is facing
            Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
            character.Move(direction * Time.fixedDeltaTime * speed);

            //New bool variable isGrounded, set to be the return value o CheckIfGrounded
            bool isGrounded = CheckIfGrounded();

            //rightDevice.TryGetFeatureValue(CommonUsages.primaryButton, out jumpBtn);

            //if (isGrounded)
            //{
            //    Debug.Log("Grounded");
            //}

            if (isGrounded && fallingSpeed.y < 0)
            {
                fallingSpeed.y = 0;
            }

            if (jumpBtn && jumpCounter < 1)
            {
                fallingSpeed.y += Mathf.Sqrt(jumpForce * -3f * gravity);
                jumpCounter++;

            }
            else if (!jumpBtn && isGrounded)
            {
                jumpCounter = 0;
            }

            fallingSpeed.y += gravity * Time.fixedDeltaTime;
            character.Move(fallingSpeed * Time.fixedDeltaTime);

        }
    }

    //Check if the player is on the ground
    bool CheckIfGrounded()
    {
        //Vector3 rayStart, set to the characters center values
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;

        //Bool hasHit, set to the value returned by the Physics.SphereCast RaycastHit
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, 
            out RaycastHit hitInfo, rayLength, groundLayer);

        return hasHit;
    }

    //Make capsule/character follow the XRrig
    void CapsuleFollowHeadset()
    {
        //Set the character height to the apropriate value
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        //new character center
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }
}
