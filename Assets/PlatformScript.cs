using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float downSpeed=3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.linearVelocity=Vector2.down*downSpeed;
    }
}
