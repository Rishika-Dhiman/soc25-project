using UnityEngine;

public class CloudScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public GameObject Camera;
    public Rigidbody2D playerRigidBody;
    public float leftVel;
    public float relDownVel;
    CameraScript camerascript;
    float leftLimit = -9;
    float rightLimit = +9;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camerascript = Camera.GetComponent<CameraScript>();        
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.linearVelocity = new Vector2(-leftVel, 0);
        if (transform.position.x < leftLimit)
        {
            transform.position = new Vector3 (rightLimit, transform.position.y, transform.position.z);
        }
        if(transform.position.y < Camera.transform.position.y - 5)
        {
            transform.position = new Vector3 (transform.position.x, Camera.transform.position.y+5, transform.position.z);
        }
        if(transform.position.y > Camera.transform.position.y + 5)
        {
            transform.position = new Vector3 (transform.position.x, Camera.transform.position.y - 5, transform.position.z);
        }
    }
}
