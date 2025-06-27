using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public SpriteRenderer mySpriteRenderer;
    public Sprite skin2;
    public Sprite skin3;
    public float runSpeed;
    public float jumpForcey;
    public float jumpForcex;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;  
    public float checkRadius=0.4f;
    public LayerMask groundLayer;
    public Vector2 boxSize = new Vector2(0.4f, 0.05f);
    public float downDispGroundCheck;

    private bool isGrounded;
    public float finishTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int selectedSkin = PlayerPrefs.GetInt("SelectedSkin",0);
        if(selectedSkin == 2)
        {
            mySpriteRenderer.sprite = skin2;
        }
        else if(selectedSkin == 3)
        {
            mySpriteRenderer.sprite = skin3;
        }
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0f;
        isGrounded = Physics2D.OverlapBox(groundCheckLeft.position+Vector3.down*downDispGroundCheck, boxSize, 0f, groundLayer) || Physics2D.OverlapBox(groundCheckRight.position + Vector3.down * downDispGroundCheck, boxSize, 0f, groundLayer);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            float dir = (transform.localScale.x)/Mathf.Abs(transform.localScale.x);
            myRigidBody.linearVelocity=new Vector2(runSpeed*dir,jumpForcey);
        }
        else if (myRigidBody.linearVelocityY <= 0 && isGrounded)
        {
            myRigidBody.linearVelocity = Vector2.zero;
        }
        if ((Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) && isGrounded)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            horizontal = -1f;
            myRigidBody.linearVelocity = new Vector2(horizontal * runSpeed, myRigidBody.linearVelocityY);
        }
       
        if ((Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) && isGrounded)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            horizontal = 1f;
            myRigidBody.linearVelocity = new Vector2(horizontal * runSpeed, myRigidBody.linearVelocityY);
        } 
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheckLeft.position + Vector3.down * downDispGroundCheck, boxSize);
        Gizmos.DrawWireCube(groundCheckRight.position + Vector3.down * downDispGroundCheck, boxSize);
    }

    public float FinishTime()
    {
        float finishTime = Time.time;
        return finishTime;
    }
}

