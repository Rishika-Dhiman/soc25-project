using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > 0)
        { 
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);    
        }
        
        
    }
}
