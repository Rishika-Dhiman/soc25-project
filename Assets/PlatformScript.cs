using Unity.VisualScripting;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public Transform Camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < Camera.position.y - 5)
        {
            Destroy(gameObject, 1f);
        }
    }
}
