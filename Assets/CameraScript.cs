using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject ground;
    Transform playerTransform;
    bool set = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("PlayerTransform",1);
         
    }

    void PlayerTransform()
    {
        playerTransform = GroundScript.localPlayer.transform;
        set = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (set && playerTransform.position.y > 0 )
        { 
            transform.position = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);    
        }
        
            
    }
}
