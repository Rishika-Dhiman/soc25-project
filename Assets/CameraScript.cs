using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public float downSpeed=2;
    bool once = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y > transform.position.y && !once)
        {
            once = true;
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }
        if (once)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + downSpeed*Time.deltaTime, transform.position.z);
        }
    }
}
