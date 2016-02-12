using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MoveSpeed = 7;
    public float RunSpeed = 15;
    public float JumpPower = 5;
    public int RotateSpeed = 100;

    [HideInInspector]
    public bool Grounded = false;

    private float speed;
    private Rigidbody myRigid;
    private bool previouslyGrounded;
    private bool isGrounded;

    [SerializeField]
    private CapsuleCollider m_Capsule;

    private Vector3 groundContactNormal;
    private bool jumping;
    private bool jump;

    private void Awake()
    {
        myRigid = transform.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * RotateSpeed * Time.deltaTime, 0);

        if (Input.GetButton("Run"))
            speed = RunSpeed;
        else
            speed = MoveSpeed;

        if (Input.GetButton("Jump") && isGrounded)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * speed;

        if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && !jumping)
        {
            if (myRigid.velocity.sqrMagnitude <
                (speed * speed))
            {
                Vector3 desiredMove = transform.forward * input.y + transform.right * input.x;
                desiredMove = Vector3.ProjectOnPlane(desiredMove, groundContactNormal).normalized;

                desiredMove.x = desiredMove.x * speed;
                desiredMove.z = desiredMove.z * speed;
                desiredMove.y = desiredMove.y * speed;
                if (myRigid.velocity.sqrMagnitude <
                    (speed * speed))
                {
                    myRigid.AddForce(desiredMove, ForceMode.Impulse);
                }
            }
        }

        if (isGrounded)
        {
            myRigid.drag = 5f;

            if (jump)
            {
                myRigid.drag = 0f;
                myRigid.velocity = new Vector3(myRigid.velocity.x, 0f, myRigid.velocity.z);
                myRigid.AddForce(new Vector3(0f, JumpPower, 0f), ForceMode.Impulse);
                jumping = true;
            }

            //if (!jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && myRigid.velocity.magnitude < 1f)
            //{
            //    myRigid.Sleep();
            //}
        }
        else
        {
            myRigid.drag = 1f;
            if (previouslyGrounded && !jumping)
            {
                StickToGroundHelper();
            }
        }
        jump = false;
    }

    private void StickToGroundHelper()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - 0), Vector3.down, out hitInfo,
                               ((m_Capsule.height / 2f) - m_Capsule.radius) +
                               0.5f, ~0, QueryTriggerInteraction.Ignore))
        {
            if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
            {
                myRigid.velocity = Vector3.ProjectOnPlane(myRigid.velocity, hitInfo.normal);
            }
        }
    }

    private void GroundCheck()
    {
        previouslyGrounded = isGrounded;
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - 0), Vector3.down, out hitInfo,
                               ((m_Capsule.height / 2f) - m_Capsule.radius) + 0.1f, ~0, QueryTriggerInteraction.Ignore))
        {
            isGrounded = true;
            groundContactNormal = hitInfo.normal;
        }
        else
        {
            isGrounded = false;
            groundContactNormal = Vector3.up;
        }
        if (!previouslyGrounded && isGrounded && jumping)
        {
            jumping = false;
        }
    }
}