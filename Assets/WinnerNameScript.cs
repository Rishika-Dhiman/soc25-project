using TMPro;
using UnityEngine;

public class WinnerNameScript : MonoBehaviour
{
    public TextMeshProUGUI winnerName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winnerName.text=Staticfile.winnerName+" wins!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
