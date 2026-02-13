using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public static int score;
    public static int plays;
    public static int cards;
    int card0;
    int card1;
    int card2;
    int card3;
    int card4;
    int card5;
    int card6;
    int card7;

    public static int[] cardArray;

    //public static bool fullSet;
    //public static bool fullSetRunning;
    //bool fullSetWaiting;
    //[SerializeField]
    //UnityEvent fullSetEvent;

    [SerializeField]
    Text playsText;
    [SerializeField]
    Text scoreText;

    [SerializeField]
    GameObject loadScreen;
    [SerializeField]
    GameObject audioSource;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();//TEMP
        //PlayerPrefs.SetInt("Full Sets", 0);//TEMP
        Physics.gravity = new Vector3(0f, -50, 0f);
        //score = PlayerPrefs.GetInt("Score", 2000000);
        plays = PlayerPrefs.GetInt("Plays", 0);
        cards = PlayerPrefs.GetInt("Cards", 0);
        card0 = PlayerPrefs.GetInt("Card0", 0);
        card1 = PlayerPrefs.GetInt("Card1", 0);
        card2 = PlayerPrefs.GetInt("Card2", 0);
        card3 = PlayerPrefs.GetInt("Card3", 0);
        card4 = PlayerPrefs.GetInt("Card4", 0);
        card5 = PlayerPrefs.GetInt("Card5", 0);
        card6 = PlayerPrefs.GetInt("Card6", 0);
        card7 = PlayerPrefs.GetInt("Card7", 0);      

        cardArray = new int[]
        {
            card0, card1, card2, card3, card4, card5, card6, card7
        };
        //fullSet = false;
        //fullSetRunning = false;

        StartCoroutine(LoadDisable());
    }

    private void Start()
    {
        playsText.text = plays.ToString();
        scoreText.text = score.ToString();
        int i = 0;
        foreach (var c in cardArray)
        {
            //Debug.Log(i + ": " + c);
            i++;
        }
    }

    private void Update()
    {
        //ebug.Log(PlayerPrefs.GetInt("Full Sets", 0));
        playsText.text = plays.ToString();
        scoreText.text = score.ToString();
    }

    IEnumerator LoadDisable()
    {
        yield return new WaitForSeconds(2f);
        loadScreen.SetActive(false);
        audioSource.SetActive(true);
    }

}
