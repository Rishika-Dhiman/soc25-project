using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.XR;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlatformSpawnerScript : MonoBehaviour
{
    public GameObject platform;
    public GameObject flag;
    public Transform Camera;
    public GameObject startButton;
    public GameObject Code;
    public float timeInterval;
    public int numberOfPlatforms;
    public float leftLimit, rightLimit;
    public float delta;
    public float heightOfFirstPlatform;
    public bool start;
    float time = 0;
    float xPos = 0;
    float yPos = -5;
    int dir;
    float jumpForcey, jumpForcex, g, R, H, l, m, a, b, y;
    int n = 0;
    public TMPro.TMP_InputField numberOfPlatformsInput;


    PhotonView photonView;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        startButton.SetActive(false);
        numberOfPlatformsInput.gameObject.SetActive(false);
        numberOfPlatforms = 30;
        if (PhotonNetwork.IsMasterClient)
        {
            
            Code.SetActive(true);
            numberOfPlatformsInput.gameObject.SetActive(true);
            string roomCode = PlayerPrefs.GetString("RoomCode", "----");
            Code.GetComponent<TextMeshProUGUI>().text = "Room Code :" + roomCode;
            Debug.Log($"Room Code : {roomCode}");
        }
        Invoke("Values", 1);
       
    }

    void Values()
    {
        time = timeInterval;
        dir = Random.value < 0.5f ? -1 : 1;
        GameObject localPlayer = GroundScript.localPlayer;
        MainCharacScript playerScript = localPlayer.GetComponent<MainCharacScript>();
        Rigidbody2D mainRigid = localPlayer.GetComponent<Rigidbody2D>();
        BoxCollider2D mainCollider = localPlayer.GetComponent<BoxCollider2D>();
        jumpForcey = playerScript.jumpForcey;
        jumpForcex = playerScript.runSpeed;
        a = localPlayer.transform.localScale.x*mainCollider.size.x;
        b = localPlayer.transform.localScale.y*mainCollider.size.y;
        g = mainRigid.gravityScale * 10;
        R = 2 * jumpForcey * jumpForcex / g;
        H = jumpForcey * jumpForcey / (2 * g);
        BoxCollider2D platformBox = platform.GetComponent<BoxCollider2D>();
        l = platformBox.size.x * platform.transform.localScale.x;
        m = platformBox.size.y * platform.transform.localScale.y / 2;
        Debug.Log($"H={H}, R={R}, g={g}, l={l}, m={m}, jumpForcey={jumpForcey}, jumpForcex={jumpForcex}, b-{b}, a={a}, delta={delta} ");
        
        yPos += heightOfFirstPlatform;
        startButton.SetActive(PhotonNetwork.IsMasterClient);
        leftLimit += 1;
        rightLimit -= 1;
        y = H;
    }
    // Update is called once per frame
    void Update()
    {
        
        float newxPos=0, newyPos=0, xRangeLeft, xRangeRight;
        

        if (time >= timeInterval && n < numberOfPlatforms && start && PhotonNetwork.IsMasterClient)
        {
            n++;
            transform.position = new Vector3(transform.position.x, Camera.position.y - 5, transform.position.z);

            PhotonNetwork.Instantiate(platform.name, Vector3.right * xPos + Vector3.up * yPos, transform.rotation);

            if (n==numberOfPlatforms)
            {
                PhotonNetwork.Instantiate(flag.name, Vector3.right * (xPos+0.3f) + Vector3.up * (yPos + 0.7f), transform.rotation);
            }
            dir = Random.value < 0.5f ? -1 : 1;

            y = Random.Range(H - y + b + 2*m + 2*delta, H);

            xRangeLeft =  a;
            xRangeRight = ((R + Mathf.Sqrt((R * R) - (4 * R * y * jumpForcex) / jumpForcey)) / 2) + l;


            if (xRangeLeft + xPos >= rightLimit - l/2)
            {
                float temp = xRangeLeft;
                xRangeLeft = xPos - xRangeRight;
                xRangeRight = xPos - temp;
            }
            else if (xPos - xRangeLeft <= leftLimit + l/2)
            {
                xRangeLeft += xPos;
                xRangeRight += xPos;
            }
            else if (dir == 1)
            {
                xRangeLeft += xPos;
                xRangeRight = xRangeRight + xPos < rightLimit ? xRangeRight + xPos : rightLimit - l/2;
            }
            else if (dir == -1)
            {
                float temp = xRangeLeft;
                xRangeLeft = xPos - xRangeRight > leftLimit ? xPos - xRangeRight : leftLimit + l/2;
                xRangeRight = xPos - temp;
            }
            newxPos = Random.Range(xRangeLeft, xRangeRight);
            newyPos = yPos + y - delta;

            Debug.Log($"Spawning platform at x={xPos}, y={yPos}, yPos={yPos}, H={H}, y={y}, xrangeLeft={xRangeLeft}, xrangeRight={xRangeRight}, m={m}, ");
            
            xPos = newxPos;
            yPos = newyPos;
            time = 0;
        }
        
        time += Time.deltaTime;
    }

    
    [PunRPC]
    public void StartGameRPC()
    {
        if(numberOfPlatformsInput.text!="" && numberOfPlatformsInput.text!="0")
        {
            numberOfPlatforms = int.Parse(numberOfPlatformsInput.text);
        }
        start = true;
        startButton.SetActive(false);
        numberOfPlatformsInput.gameObject.SetActive(false);
        Code.SetActive(false);
    }
    public void StartGame()
    {
        photonView.RPC("StartGameRPC",RpcTarget.All);
    }
}
