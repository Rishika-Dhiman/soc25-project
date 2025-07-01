using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject ground;
    Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
         playerTransform=GroundScript.localPlayer.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerTransform == null)
        {
            // Wait until localPlayer is assigned
            if (GroundScript.localPlayer != null)
            {
                playerTransform = GroundScript.localPlayer.transform;
            }
            return;
        }

        if (playerTransform.position.y > 0)
        { 
            transform.position = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);    
        }
        
            
    }
}
