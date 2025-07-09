using UnityEngine;
using Photon.Pun;

public class GroundScript : MonoBehaviourPunCallbacks

{
    public GameObject ground;
    public GameObject player;
    public static GameObject localPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(ground, transform.position + Vector3.right*2,transform.rotation);

        Vector3 spawnPos = new Vector3(Random.Range(-2f, 2f), -1f, 0f);

        int skinIndex = PlayerPrefs.GetInt("SelectedSkin", 0);
        object[] instantiationData = new object[] {skinIndex};
        localPlayer = PhotonNetwork.Instantiate(player.name, spawnPos, Quaternion.identity,0,instantiationData);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
