using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatingUI : MonoBehaviour
{
    private int numOfLives = 6;
    private int numOfSpecialNotes = 3;
    [SerializeField] private GameObject heart1, heart2, heart3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseLives()
    {
        numOfLives++;
        Debug.Log("Increased lives to " + numOfLives);
        switch (numOfLives)
        {
            case 7:
                numOfLives--;
                break;
            case 5:
                heart3.SetActive(true);
                break;
            case 3:
                heart2.SetActive(true);
                break;
            case 1:
                heart1.SetActive(true);
                break;
            //default:
                //break;
        }

    }

    public void DecreaseLives()
    {
        numOfLives--;
        Debug.Log("decreased lives to " + numOfLives);
        switch (numOfLives)
        {
            case 4:
                heart3.SetActive(false);
                break;
            case 2:
                heart2.SetActive(false);
                break;
            case 0:
                heart1.SetActive(false);
                Debug.Log("You lose");
                break;
            //default:
            //break;
            case -1:
                numOfLives++; //dont let it go below 0 for test purposes.
                break;
        }
    }
}
