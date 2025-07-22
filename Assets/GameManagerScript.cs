using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class GameManagerScript : MonoBehaviourPunCallbacks
{
    
    public TMPro.TMP_InputField playerName, gameCode;
    public GameObject createRoom;
    public GameObject joinRoom;
    bool active = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        createRoom.SetActive(false);
        joinRoom.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (!active && PhotonNetwork.IsConnectedAndReady)
        //{
            
        //    createRoom.SetActive(true);
        //    joinRoom.SetActive(true);
        //    active = true;
        //}
    }
    public override void OnConnectedToMaster()
    {
        createRoom.SetActive(true);
        joinRoom.SetActive(true);
    }

    public void CreateRoom()
    {
        PhotonNetwork.NickName = playerName.text;
        string roomCode = Random.Range(1000, 9999).ToString();
        PlayerPrefs.SetString("RoomCode", roomCode);
        PlayerPrefs.Save();
        PhotonNetwork.CreateRoom(roomCode);
        Debug.Log($"Room Code : {roomCode}");
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
