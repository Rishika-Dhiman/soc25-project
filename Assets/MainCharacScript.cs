using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float u0;
    public float runSpeed;
    public float jumpForcey;
    public float jumpForcex;

    public Transform groundCheck;
    public float checkRadius=0.4f;
    public LayerMask groundLayer;
    private Vector2 velocity;
    public Vector2 boxSize = new Vector2(0.4f, 0.05f);
    public float downDispGroundCheck;

    private bool isGrounded;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0f;
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position,checkRadius, groundLayer);
        isGrounded = Physics2D.OverlapBox(groundCheck.position+Vector3.down*downDispGroundCheck, boxSize, 0f, groundLayer);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            float dir = (transform.localScale.x)/Mathf.Abs(transform.localScale.x);
            //velocity = Vector2.one*u0;
            //velocity.x= dir*u0;
            //myRigidBody.linearVelocity = velocity;
            myRigidBody.linearVelocity=new Vector2(runSpeed*dir,jumpForcey);
        }
        else if (myRigidBody.linearVelocityY <= 0 && isGrounded)
        {
            myRigidBody.linearVelocity = Vector2.zero;
        }
        if (Keyboard.current.aKey.isPressed && isGrounded)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            horizontal = -1f;
            myRigidBody.linearVelocity = new Vector2(horizontal * runSpeed, myRigidBody.linearVelocityY);
            //transform.localPosition+=Vector3.left*runSpeed*Time.deltaTime;
        }
       
        if (Keyboard.current.dKey.isPressed && isGrounded)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            horizontal = 1f;
            myRigidBody.linearVelocity = new Vector2(horizontal * runSpeed, myRigidBody.linearVelocityY);
            //transform.localPosition+= Vector3.right * runSpeed*Time.deltaTime;
        }

        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position+Vector3.down * downDispGroundCheck, boxSize);
    }
}

