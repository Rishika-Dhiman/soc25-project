using UnityEngine;

public class GroundScript : MonoBehaviour

{
    public GameObject ground;
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
