using UnityEngine;

public class GroundScript : MonoBehaviour

{
    public Rigidbody2D myRigidbody;
    public GameObject ground;
    public float startTime;
    public float downSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(ground, transform.position,transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
