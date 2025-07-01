using Unity.VisualScripting;
using UnityEngine;

public class FlagScript : MonoBehaviour
{

    GameObject localPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        localPlayer= GroundScript.localPlayer; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            MainCharacScript playerScript = localPlayer.GetComponent<MainCharacScript>();
            playerScript.finishTime=playerScript.FinishTime();
            Destroy(gameObject);
        }
    }
}
