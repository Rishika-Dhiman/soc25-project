using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class GameManagerScript : MonoBehaviourPunCallbacks
{
    
    public TMPro.TMP_InputField playerName, gameCode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CreateRoom()
    {
        PhotonNetwork.NickName = playerName.text;
        string roomCode = Random.Range(1000, 9999).ToString();
        PhotonNetwork.CreateRoom(roomCode);
    }

    public void JoinRoom()
    {
        PhotonNetwork.NickName= playerName.text;
        PhotonNetwork.JoinRoom(gameCode.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }
    public void selectSkin(int n)
    {
        PlayerPrefs.SetInt("SelectedSkin", n);
    }
}
