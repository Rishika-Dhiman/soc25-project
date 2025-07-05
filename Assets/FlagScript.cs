using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

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
            MainCharacScript playerScript = other.GetComponent<MainCharacScript>();
            playerScript.finishTime=playerScript.FinishTime();
            Debug.Log($"Finish time ");
            Staticfile.winnerName = playerScript.nickname;
            PhotonNetwork.LoadLevel("EndScene");
            Destroy(gameObject);
        }
    }
}
