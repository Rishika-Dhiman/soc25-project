using UnityEngine;

public class GroundScript : MonoBehaviour

{
    public Rigidbody2D myRigidbody;
    public float startTime;
    public float downSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;

        if (time > startTime)
        {
            myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            myRigidbody.linearVelocity = Vector2.down * downSpeed;
        }
    }
}
