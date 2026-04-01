using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlaySceneManager : MonoBehaviour
{
    public static int score;
    public static float credits;
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
    bool isBroke;
    [SerializeField]
    GameObject creditAdder;

    [SerializeField]
    GameObject loadScreen;
    [SerializeField]
    GameObject audioSource;

    float timePlayed;
    public static string timeFormatted;
    ///Save/Load
    [SerializeField]
    GameObject startObjects;
    SaveData1 saveData;
    GameObject[] saveElaut;
    GameObject[] saveTrooper;
    GameObject[] saveVader;
    GameObject[] saveYoda;
    GameObject[] saveCard;
    GameObject[] saveGold;
    GameObject[][] saveIndObjects;
    string[] saveIndObjecttypes;
    List<GameObject> saveObjects;
    string[] saveTypes;
    [SerializeField]
    GameObject[] saveObjectPrefabs;
    [SerializeField]
    Transform saveParent;
    [SerializeField]
    GameObject[] cardSavePrefabs;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();//TEMP
        //PlayerPrefs.SetInt("Full Sets", 100);//TEMP
        Physics.gravity = new Vector3(0f, -50, 0f);
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        score = PlayerPrefs.GetInt("Score", 0);
        credits = PlayerPrefs.GetFloat("Credits", 50f);
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

        if(credits < 1)
        {
            isBroke = true;
            creditAdder.SetActive(true);
        }

        timePlayed = PlayerPrefs.GetFloat("Total Time", 0f);

        ///Save
        
        saveData = new SaveData1();
        saveTypes = new string[]
        {
            "Elaut", "Trooper", "Vader", "Yoda", "Card", "Gold"
        };
        saveObjects = new List<GameObject>();
        saveIndObjects = new GameObject[][]
        {
            saveElaut, saveTrooper, saveVader, saveYoda, saveCard, saveGold
        };
        saveIndObjecttypes = new string[]
        {
            "Elaut", "Trooper", "Vader", "Yoda", "Card", "Gold"
        };
        StartCoroutine(LoadDisable());
    }

    private void Start()
    {
        for (int j = 0; j < saveTypes.Length; j++)
        {
            saveData.LoadObject(saveObjectPrefabs[j], cardSavePrefabs, saveObjectPrefabs[j].tag, saveParent);

        }
        if (PlayerPrefs.GetInt("New Game", 0) == 0)
        {
            PlayerPrefs.SetInt("New Game", 1);
            startObjects.SetActive(true);
        }
        playsText.text = plays.ToString();
        scoreText.text = score.ToString();
        int i = 0;
        foreach (var c in cardArray)
        {
            //Debug.Log(j + ": " + c);
            i++;
        }
    }

    private void Update()
    {
        //Debug.Log(PlayerPrefs.GetInt("Full Sets", 0));
        playsText.text = plays.ToString();
        scoreText.text = score.ToString();

        if (!isBroke)
        {
            if(credits < 1)
            {
                isBroke = true;
                creditAdder.SetActive(true);
            }
        }
        //Stats
        timePlayed += Time.deltaTime;
        PlayerPrefs.SetFloat("Total Time", timePlayed);
        int seconds = Mathf.FloorToInt(timePlayed);
        int minutes = seconds / 60;
        var timeSpan = TimeSpan.FromSeconds(timePlayed);
        timeFormatted = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        //Debug.Log("Time: " +  timeFormatted);
    }

    IEnumerator LoadDisable()
    {
        yield return new WaitForSeconds(2f);
        loadScreen.SetActive(false);
        audioSource.SetActive(true);
    }

    void SaveCardAmounts()
    {
        for(int i = 0; i < cardArray.Length; i++)
        {
            PlayerPrefs.SetInt("Card" + i, cardArray[i]);
        }
    }
    void GatherSaveobjects()
    {
        for (int i = 0; i < saveIndObjects.Length; i++)
        {
            if (saveTypes[i] == "Card")
            {
                saveIndObjects[i] = GameObject.FindGameObjectsWithTag(saveTypes[i]);
            }
            else
            {
                saveIndObjects[i] = GameObject.FindGameObjectsWithTag(saveTypes[i] + "Save");
            }
        }
    }

    void SaveObjects()
    {
        for (int i = 0; i < saveIndObjects.Length; i++)
        {
            for (int j = 0; j < saveIndObjects[i].Length; j++)
            {
                saveData.SaveObject(saveIndObjects[i][j], saveIndObjects[i][j].tag);
                //Debug.Log("Tag: " + saveIndObjects[i][j].tag);
            }
        }
    }

    public void Save()
    {
        SaveCardAmounts();
        GatherSaveobjects();
        SaveObjects();
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Plays", plays);
        PlayerPrefs.SetFloat("Credits", credits);
    }
    private void OnApplicationQuit()
    {
        Save();
        //PlayerPrefs.DeleteAll();//TEMP
    }
}
