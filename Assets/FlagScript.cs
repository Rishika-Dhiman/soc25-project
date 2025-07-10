using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class FlagScript : MonoBehaviour
{
    PhotonView photonView;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void SetWinnerRPC(string winnerName)
    {
        Staticfile.winnerName = winnerName;
        PhotonNetwork.LoadLevel("EndScene");
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            MainCharacScript playerScript = other.GetComponent<MainCharacScript>();
            playerScript.finishTime=playerScript.FinishTime();
            Debug.Log($"Finish time ");
            PhotonView view = other.GetComponent<PhotonView>();
            string winnerName = view.Owner.NickName;
            photonView.RPC("SetWinnerRPC", RpcTarget.All,winnerName);
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("EndScene");
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
