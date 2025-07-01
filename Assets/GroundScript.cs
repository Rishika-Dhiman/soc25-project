using UnityEngine;
using Photon.Pun;

public class GroundScript : MonoBehaviourPunCallbacks

{
    public GameObject ground;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(ground, transform.position + Vector3.right*2,transform.rotation);

        Vector3 spawnPos = new Vector3(Random.Range(-2f, 2f), 3f, 0f);
        PhotonNetwork.Instantiate(player.name, spawnPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
