using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RewardController : MonoBehaviour
{
    public static List<string> popUpList;
    public static bool isPopUp;

    [SerializeField]
    GameObject milestonePanel;
    [SerializeField]
    Animator milestonePanelAnim;
    [SerializeField]
    bool closed;

    [SerializeField]
    Text pointsText;
    [SerializeField]
    Text blurbText;
    [SerializeField]
    Image sexyPic;
    [SerializeField]
    Sprite[] ladyPics;

    [SerializeField]
    GameObject fullpanel;

    [SerializeField]
    GameObject rewardPopup;

    ///SCORE/////
    int fiveK;
    int tenK;
    int twentyFiveK;
    int fiftyK;
    int seventyFiveK;
    int hundredK;
    int twofiftyK;
    int fiveHundredK;
    int million;
    int[] points;

    string fiveKName;
    string tenKName;
    string twentyFiveKName;
    string fiftyKName;
    string seventyFiveKName;
    string hundredKName;
    string twofiftyKName;
    string fiveHundredKName;
    string millionName;
    string[] pointsName;

    int fiveKScore;
    int tenKScore;
    int twentyFiveKScore;
    int fiftyKScore;
    int seventyFiveKScore;
    int hundredKScore;
    int twofiftyKScore;
    int fiveHundredKScore;
    int millionScore;
    int[] pointsScore;

    ///FULL SETS
    int threeSets;
    int fiveSets;
    int tenSets;
    int twentyFiveSets;
    int fiftySets;
    int hundredSets;
    int[] setInts;

    string threeSetsName;
    string fiveSetsName;
    string tenSetsName;
    string twentyFiveSetsName;
    string fiftySetsName;
    string hundredSetsName;
    string[] setNames;

    int threeSetsValue;
    int fiveSetsValue;
    int tenSetsValue;
    int twentyFiveSetsValue;
    int fiftySetsValue;
    int hundredSetsValue;
    int[] setValues;

    [SerializeField]
    UnityEvent fullSetEvent;

    [SerializeField]
    UnityEvent checkGoals;

    int buyAll;

    ///GOLD
    [SerializeField]
    UnityEvent goldEvent;

    ///SABER
    [SerializeField]
    UnityEvent saberEvent;

    ///ALL GOALS
    [SerializeField]
    UnityEvent allGoalsEvent;

    private void Awake()
    {
        closed = false;
        milestonePanelAnim.SetBool("closed", closed);

        popUpList = new List<string>();

        ///SCORES
        fiveK = PlayerPrefs.GetInt("5K", 0);
        tenK = PlayerPrefs.GetInt("10K", 0);
        twentyFiveK = PlayerPrefs.GetInt("25K", 0);
        fiftyK = PlayerPrefs.GetInt("50K", 0);
        seventyFiveK = PlayerPrefs.GetInt("75K", 0);
        hundredK = PlayerPrefs.GetInt("100K", 0);
        twofiftyK = PlayerPrefs.GetInt("250K", 0);
        fiveHundredK = PlayerPrefs.GetInt("500K", 0);
        million = PlayerPrefs.GetInt("Million", 0);

        fiveKName = "5K";
        tenKName = "10K";
        twentyFiveKName = "25K";
        fiftyKName = "50K";
        seventyFiveKName = "75K";
        hundredKName = "100K";
        twofiftyKName = "250K";
        fiveHundredKName = "500K";
        millionName = "Million";

        fiveKScore = 5000;
        tenKScore = 10000;
        twentyFiveKScore = 25000;
        fiftyKScore = 50000;
        seventyFiveKScore = 75000;
        hundredKScore = 100000;
        twofiftyKScore = 250000;
        fiveHundredKScore = 500000;
        millionScore = 1000000;

        points = new int[]
        {
            fiveK,
            tenK,
            twentyFiveK,
            fiftyK,
            seventyFiveK,
            hundredK,
            twofiftyK,
            fiveHundredK,
            million
        };
        pointsName = new string[]
        {
            fiveKName,
            tenKName,
            twentyFiveKName,
            fiftyKName,
            seventyFiveKName,
            hundredKName,
            twofiftyKName,
            fiveHundredKName,
            millionName
        };
        pointsScore = new int[]
        {
            fiveKScore,
            tenKScore,
            twentyFiveKScore,
            fiftyKScore,
            seventyFiveKScore,
            hundredKScore,
            twofiftyKScore,
            fiveHundredKScore,
            millionScore
        };
        
        

        ///FULL SETS
        ///
        //fullSet = false;
        threeSets = PlayerPrefs.GetInt("3Sets", 0);
        fiveSets = PlayerPrefs.GetInt("5Sets", 0);
        tenSets = PlayerPrefs.GetInt("10Sets", 0);
        twentyFiveSets = PlayerPrefs.GetInt("25Sets", 0);
        fiftySets = PlayerPrefs.GetInt("50Sets", 0);
        hundredSets = PlayerPrefs.GetInt("100Sets", 0);

        threeSetsName = "3Sets";
        fiveSetsName = "5Sets";
        tenSetsName = "10Sets";
        twentyFiveSetsName = "25Sets";
        fiftySetsName = "50Sets";
        hundredSetsName = "100Sets";

        threeSetsValue = 3;
        fiveSetsValue = 5;
        tenSetsValue = 10;
        twentyFiveSetsValue = 25;
        fiftySetsValue = 50;
        hundredSetsValue = 100;

        setInts = new int[]
        {
            threeSets,
            fiveSets,
            tenSets,
            twentyFiveSets,
            fiftySets,
            hundredSets
        };
        setNames = new string[]
        {
            threeSetsName,
            fiveSetsName,
            tenSetsName,
            twentyFiveSetsName,
            fiftySetsName,
            hundredSetsName
        };
        setValues = new int[]
        {
            threeSetsValue,
            fiveSetsValue,
            tenSetsValue,
            twentyFiveSetsValue,
            fiftySetsValue,
            hundredSetsValue
        };

        buyAll = PlayerPrefs.GetInt("BuyAll", 0);

        ///GOLD SHOOTER

    }

    private void Start()
    {
        CheckPoints();
        CheckFullSets();
        StartCoroutine(PopUpController());
    }

    private void Update()
    {
        ////MILESTONE PANEL///
        milestonePanelAnim.SetBool("closed", closed);
        //Debug.Log("Closed: " + closed);

        ///SCORES///////
        if (fiveK == 0)
        {
            if (PlaySceneManager.score >= 5000)
            {
                fiveK = 1;
                popUpList.Add("5K");
                //Debug.Log("5K");
            }
        }
        if (tenK == 0)
        {
            if (PlaySceneManager.score >= 10000)
            {
                tenK = 1;
                popUpList.Add("10K");
                //Debug.Log("10K");
            }
        }
        if (twentyFiveK == 0)
        {
            if (PlaySceneManager.score >= 25000)
            {
                twentyFiveK = 1;
                popUpList.Add("25K");
                //Debug.Log("25K");
            }
        }
        if (fiftyK == 0)
        {
            if (PlaySceneManager.score >= 50000)
            {
                fiftyK = 1;
                popUpList.Add("50K");
                //Debug.Log("50K");
            }
        }
        if (seventyFiveK == 0)
        {
            if (PlaySceneManager.score >= 75000)
            {
                seventyFiveK = 1;
                popUpList.Add("75K");
                //Debug.Log("75K");
            }
        }
        if (hundredK == 0)
        {
            if (PlaySceneManager.score >= 100000)
            {
                hundredK = 1;
                popUpList.Add("100K");
                //Debug.Log("100K");
            }
        }
        if (twofiftyK == 0)
        {
            if (PlaySceneManager.score >=250000)
            {
                twofiftyK = 1;
                popUpList.Add("250K");
                //Debug.Log("250K");
            }
        }
        if (fiveHundredK == 0)
        {
            if (PlaySceneManager.score >= 500000)
            {
                fiveHundredK = 1;
                popUpList.Add("500K");
                //Debug.Log("500K");
            }
        }
        if (million == 0)
        {
            if (PlaySceneManager.score >= 1000000)
            {
                million = 1;
                popUpList.Add("Million");
                //Debug.Log("Million");
            }
        }




        ////FULL SETS

    }

    void CheckPoints()
    {
        for(int i = 0; i < points.Length; i++)
        {
            if (points[i] == 0 && PlaySceneManager.score >= pointsScore[i])
            {
                PlayerPrefs.SetInt(pointsName[i], 1);
                checkGoals.Invoke();
                //Debug.Log("Point Name: " + pointsName[i]);
            }
        }
    }

    public void CloseMenu()
    {
        closed = true;
    }
    
    ///FULL SETS    
    
    public void CheckFullSets()
    {
        for (int i = 0; i < setInts.Length; i++)
        {
            if (setInts[i] == 0)
            {
                if(PlayerPrefs.GetInt("Full Sets", 0)  >= setValues[i])
                {
                    //Debug.Log("Value: " + setValues[i]);
                    PlayerPrefs.SetInt(setNames[i], 1);
                    setInts[i] = 1;
                    rewardPopup.SetActive(true);
                }
            }
        }
    }

    ///POP UPS
    IEnumerator PopUpController()
    {
        while(true)
        {
            while(popUpList.Count > 0)
            {
                //Debug.Log("Waiting..");
                if (!isPopUp)
                {
                    isPopUp = true;
                    PopUpResult(popUpList[0]);
                    //Debug.Log(popUpList[0]);
                    popUpList.RemoveAt(0);
                }
                yield return null;
            }
            yield return null;
        }
    }
    void PopUpResult(string name)
    {
        switch(name)
        {
            case "5K":
                PlayerPrefs.SetInt("5K", 1);
                pointsText.text = 5000 + " POINTS";
                blurbText.text = "WOW, GREAT JOB CUTIE!";
                int i = Random.Range(0, ladyPics.Length);
                sexyPic.sprite = ladyPics[i];
                closed = false;
                milestonePanel.SetActive(true);
                rewardPopup.SetActive(true);
                checkGoals.Invoke();
                break;

            case "10K":
                PlayerPrefs.SetInt("10K", 1);
                pointsText.text = 10000 + " POINTS";
                blurbText.text = "YOU'RE TALENTED AND GOOD LOOKING!";
                int j = Random.Range(0, ladyPics.Length);
                sexyPic.sprite = ladyPics[j];
                closed = false;
                milestonePanel.SetActive(true);
                rewardPopup.SetActive(true);
                checkGoals.Invoke();
                break;

            case "25K":
                PlayerPrefs.SetInt("25K", 1);
                pointsText.text = 25000 + " POINTS";
                blurbText.text = "NOW YOU'RE GETTING ME EXCITED!";
                int k = Random.Range(0, ladyPics.Length);
                sexyPic.sprite = ladyPics[k];
                closed = false;
                milestonePanel.SetActive(true);
                rewardPopup.SetActive(true);
                checkGoals.Invoke();
                break;

            case "50K":
                PlayerPrefs.SetInt("50K", 1);
                pointsText.text = 50000 + " POINTS";
                blurbText.text = "I'M SO IMPRESSED! ARE YOU SINGLE?";
                int l = Random.Range(0, ladyPics.Length);
                sexyPic.sprite = ladyPics[l];
                closed = false;
                milestonePanel.SetActive(true);
                rewardPopup.SetActive(true);
                checkGoals.Invoke();
                break;

            case "75K":
                PlayerPrefs.SetInt("75k", 1);
                pointsText.text = 75000 + " POINTS";
                blurbText.text = "THAT'S A LOT OF POINTS\r\nYOU'RE GONNA BE FAMOUS!";
                int m = Random.Range(0, ladyPics.Length);
                sexyPic.sprite = ladyPics[m];
                closed = false;
                milestonePanel.SetActive(true);
                rewardPopup.SetActive(true);
                checkGoals.Invoke();
                break;

            case "100K":
                PlayerPrefs.SetInt("100K", 1);
                pointsText.text = 100000 + " POINTS";
                blurbText.text = "YOU'RE SO AMAZING\r\nI CAN BARELY CONTAIN MYSELF!";
                int n = Random.Range(0, ladyPics.Length);
                sexyPic.sprite = ladyPics[n];
                closed = false;
                milestonePanel.SetActive(true);
                rewardPopup.SetActive(true);
                checkGoals.Invoke();
                break;

            case "250K":
                PlayerPrefs.SetInt("250K", 1);
                pointsText.text = 250000 + " POINTS";
                blurbText.text = "DANG, YOU'RE KILLING IT\r\nI THINK I'M FALLING IN LOVE!";
                int o = Random.Range(0, ladyPics.Length);
                sexyPic.sprite = ladyPics[o];
                closed = false;
                milestonePanel.SetActive(true);
                rewardPopup.SetActive(true);
                checkGoals.Invoke();
                break;

            case "500K":
                PlayerPrefs.SetInt("500K", 1);
                pointsText.text = 500000 + " POINTS";
                blurbText.text = "COULD YOU BE ANY MORE TALENTED?\r\nI CAN'T HANDLE MUCH MORE!";
                int p = Random.Range(0, ladyPics.Length);
                sexyPic.sprite = ladyPics[p];
                closed = false;
                milestonePanel.SetActive(true);
                rewardPopup.SetActive(true);
                checkGoals.Invoke();
                break;

            case "Million":
                PlayerPrefs.SetInt("Million", 1);
                pointsText.text = 1000000 + " POINTS";
                blurbText.text = "DAMN, THAT'S SEXY AF!\r\nLET'S GO BACK TO MY PLACE";
                int q = Random.Range(0, ladyPics.Length);
                sexyPic.sprite = ladyPics[q];
                closed = false;
                milestonePanel.SetActive(true);
                rewardPopup.SetActive(true);
                checkGoals.Invoke();
                break;

            case "Full Set":
                int fs = PlayerPrefs.GetInt("Full Sets", 0);
                PlayerPrefs.SetInt("Full Sets", fs + 1);
                CheckFullSets();
                fullSetEvent.Invoke();
                checkGoals.Invoke();
                break;

            case "Gold":
                GoldCoinController.isBonusTime = true;
                goldEvent.Invoke();
                break;

            case "Sabers":
                saberEvent.Invoke();
                break;

            case "All Goals":
                PlayerPrefs.SetInt("All Goals", 1);
                allGoalsEvent.Invoke();
                checkGoals.Invoke();
                break;
        }
    }
}
