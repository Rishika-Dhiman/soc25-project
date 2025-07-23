using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacScript : MonoBehaviourPun, IPunObservable
{
    public Rigidbody2D myRigidBody;
    public SpriteRenderer mySpriteRenderer;
    public Sprite skin2;
    public Sprite skin3;
    public float runSpeed;
    public float jumpForcey;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public float checkRadius = 0.4f;
    public LayerMask groundLayer;
    public Vector2 boxSize = new Vector2(0.4f, 0.05f);
    public float downDispGroundCheck;

    private bool isGrounded;
    public float finishTime;
    float startTime;

    public string nickname;

    private Vector2 networkPosition;
    private Vector2 networkVelocity;

    void Start()
    {
        nickname = PhotonNetwork.NickName;

        int selectedSkin = (int)photonView.InstantiationData[0];
        if (selectedSkin == 2)
        {
            mySpriteRenderer.sprite = skin2;
        }
        else if (selectedSkin == 3)
        {
            mySpriteRenderer.sprite = skin3;
        }

        PlayerPrefs.DeleteAll();
        startTime = Time.time;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(myRigidBody.position);
            stream.SendNext(myRigidBody.linearVelocity);
        }
        else
        {
            networkPosition = (Vector2)stream.ReceiveNext();
            networkVelocity = (Vector2)stream.ReceiveNext();
        }
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            myRigidBody.position = Vector2.Lerp(myRigidBody.position, networkPosition, 0.2f);
            myRigidBody.linearVelocity = Vector2.Lerp(myRigidBody.linearVelocity, networkVelocity, 0.1f);
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        float horizontal = 0f;

        isGrounded =
            Physics2D.OverlapBox(groundCheckLeft.position + Vector3.down * downDispGroundCheck, boxSize, 0f, groundLayer)
            || Physics2D.OverlapBox(groundCheckRight.position + Vector3.down * downDispGroundCheck, boxSize, 0f, groundLayer);


        float dir;

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            dir = transform.localScale.x / Mathf.Abs(transform.localScale.x);
            myRigidBody.linearVelocity = new Vector2(0, jumpForcey);
        }
        

        if ((Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) )
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            horizontal = -1f;
            myRigidBody.linearVelocity = new Vector2(horizontal * runSpeed, myRigidBody.linearVelocity.y);
        }

        if ((Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) )
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            horizontal = 1f;
            myRigidBody.linearVelocity = new Vector2(horizontal * runSpeed, myRigidBody.linearVelocity.y);
        }

        if (myRigidBody.linearVelocityX > 0)
        {
            myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocityX - 0.01f, myRigidBody.linearVelocityY);
        }
        if (myRigidBody.linearVelocityX < 0)
        {
            myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocityX + 0.01f, myRigidBody.linearVelocityY);
        }
        if (transform.position.x > 9 && myRigidBody.linearVelocityX > 0 )
        {
            myRigidBody.linearVelocity = new Vector2(-myRigidBody.linearVelocityX, myRigidBody.linearVelocityY);
        }
        if (transform.position.x < -9 &&  myRigidBody.linearVelocityX < 0)
        {
            myRigidBody.linearVelocity = new Vector2(-myRigidBody.linearVelocityX, myRigidBody.linearVelocityY);
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
        return Time.time - startTime;
    }
}
