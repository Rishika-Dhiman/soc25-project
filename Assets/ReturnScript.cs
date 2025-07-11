using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class ReturnScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnTOMain()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("StartScreen");
    }

}
