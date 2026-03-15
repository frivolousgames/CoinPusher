using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CollectController : MonoBehaviour
{
    [SerializeField]
    Text scoreAddObject;
    [SerializeField]
    GameObject mainCanvas;
    int scoreAmount;
    [SerializeField]
    AudioClip coinClip;
    [SerializeField]
    AudioClip yodaClip;
    [SerializeField]
    AudioClip goldClip;
    [SerializeField]
    AudioClip cardClip;
    AudioClip chosenClip;
    [SerializeField]
    AudioSource collectPlayer;
    float minX;
    float maxX;

    [SerializeField]
    Transform cardAdderTrans;
    [SerializeField]
    GameObject cardAdder;
    [SerializeField]
    Sprite[] cardSprites;
    int cardNum;

    int type;
    [SerializeField]
    GameObject[] types;
    [SerializeField]
    GameObject[] cards;
    GameObject selectedObject;
    List<GameObject> previousCards;
    float minPosX;
    float maxPosX;
    [SerializeField]
    Transform spawnParent;
    [SerializeField]
    Transform shooterSpawn;
    [SerializeField]
    Transform shooter;
    List<GameObject> spawnedObjects;
    Vector3 shooterStartL;
    Vector3 shooterStartR;
    bool left;
    bool spawning;
    [SerializeField]
    float spawnerSpeed;

    [SerializeField]
    UnityEvent cardMenuCheck;

    public static int[] startingObjectNums;
    int vaderStartNum;
    int trooperStartNum;
    int yodaStartNum;
    int cardStartNum;
    int selectedStartNum;
    int[] startingObjectNumLimits;

    [SerializeField]
    AudioSource spawnSound;
    [SerializeField]
    AudioClip[] cardSpawnClips;
    [SerializeField]
    AudioClip yodaSpawnClip;
    [SerializeField]
    AudioClip coinSpawnClip;
    List<AudioClip> spawnedObjectClips;
    AudioClip chosenSpawnClip;

    //Gold Coin
    float goldCoinTime;
    float maxGoldCoinTime;
    public static int goldStartNum;
    [SerializeField]
    GameObject goldCoinObject;
    [SerializeField]
    AudioClip goldSpawnClip;

    private void Awake()
    {
        minX = -100f;
        maxX = 100f;
        cardNum = 0;

        minPosX = -4.5f;
        maxPosX = 6.9f;
        shooterStartL = new Vector3(-17.5f, 15.1f, 5.85f);
        shooterStartR = new Vector3(20f, 15.1f, 5.85f);
        left = true;
        spawning = false;
        shooter.position = shooterStartL;

        //-17.5, 20

        selectedObject = null;
        previousCards = new List<GameObject>();
        spawnedObjects = new List<GameObject>();
        spawnedObjectClips = new List<AudioClip>();

        trooperStartNum = 26;
        vaderStartNum = 25;
        yodaStartNum = 1;
        cardStartNum = 5;

        startingObjectNums = new int[]
        {
            100, PlayerPrefs.GetInt("TrooperSave", 26), PlayerPrefs.GetInt("VaderSave", 25), PlayerPrefs.GetInt("YodaSave", 1), PlayerPrefs.GetInt("CardStartNum", 5), PlayerPrefs.GetInt("GoldSave", 0)
        };

        startingObjectNumLimits = new int[]
        {
            0, trooperStartNum, vaderStartNum, yodaStartNum, cardStartNum, goldStartNum
        };
        
        //Gold Coin
        goldStartNum = 0;
        SetGoldCoinTime();
        AddGoldCoin();
        CheckStartingAmounts();
    }

    private void Update()
    {
        //Debug.Log("GCTime: " + goldCoinTime);
        //Debug.Log("GCs: " + goldStartNum);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trooper"))
        {
            scoreAmount = 10;
            SceneManager.score += 10;
            chosenClip = coinClip;
            type = 0;
            startingObjectNums[1]--;
            selectedStartNum = trooperStartNum;
            Destroy(other.transform.parent.gameObject);
            SpawnObjects();
        }
        else if (other.CompareTag("Vader"))
        {
            scoreAmount = 25;
            SceneManager.score += 25;
            chosenClip = coinClip;
            type = 1;
            startingObjectNums[2]--;
            selectedStartNum = vaderStartNum;
            Destroy(other.transform.parent.gameObject);
            SpawnObjects();
        }
        else if (other.CompareTag("Yoda"))
        {
            scoreAmount = 500;
            SceneManager.score += 500;
            chosenClip = yodaClip;
            type = 2;
            startingObjectNums[3]--;
            selectedStartNum = yodaStartNum;
            Destroy(other.transform.parent.gameObject);
            SpawnObjects();
        }

        else if (other.CompareTag("Elaut"))
        {
            scoreAmount = 2;
            SceneManager.score += 2;
            chosenClip = coinClip;
            Destroy(other.transform.parent.gameObject);
        }
        else if (other.CompareTag("Gold"))
        {
            scoreAmount = 100;
            SceneManager.score += 100;
            chosenClip = goldClip;
            RewardController.popUpList.Add("Gold");
            startingObjectNums[5]--;
            AddGoldCoin();
            Destroy(other.transform.parent.gameObject);
        }
        else
        {
            scoreAmount = 100;
            SceneManager.score += 100;
            chosenClip = cardClip;
            type = 3;
            startingObjectNums[4]--;
            selectedStartNum = cardStartNum;
            GameObject card = Instantiate(cardAdder, cardAdderTrans);
            int.TryParse(other.gameObject.name, out cardNum);
            card.GetComponent<Image>().sprite = cardSprites[cardNum];
            SceneManager.cardArray[cardNum]++;
            Destroy(other.gameObject);
            SpawnObjects();
            cardMenuCheck.Invoke();
            CheckCardAmount();
            //int i = 0;
            //foreach (var c in SceneManager.cardArray)
            //{
            //    Debug.Log(i + ": " + c);
            //    i++;
            //}
        }
        //Debug.Log(other.tag);
        float xPos = Random.Range(minX, maxX);
        Vector3 pos = new Vector3(xPos, 0, 0);
        Text scorePrefab = Instantiate(scoreAddObject, mainCanvas.transform);
        scorePrefab.text = "+" + scoreAmount.ToString();
        scorePrefab.transform.localPosition = pos;
        collectPlayer.PlayOneShot(chosenClip);
        
    }

    void CheckCardAmount()
    {
        foreach (var card in SceneManager.cardArray)
        {
            if (card > 0)
            {

            }
            else
            {
                return;
            }
        }
        RewardController.popUpList.Add("Full Set");
        SubtractFullSetCards();
        //Debug.Log("Subtracting");
    }

    void SubtractFullSetCards()
    {
        for(int i = 0; i < SceneManager.cardArray.Length; i++)
        {
            SceneManager.cardArray[i]--;
        }
        cardMenuCheck.Invoke();
    }

    void PickNextCard()
    {
        if(previousCards.Count > 20)
        {
            foreach(var card in previousCards)
            {
                if(card == cards[7])
                {
                    previousCards.Clear(); 
                    PickNextCard();
                    Debug.Log("Vader found");
                    return;
                }
                else
                {
                    selectedObject = cards[7];
                    chosenSpawnClip = cardSpawnClips[7];
                    previousCards.Clear();
                    Debug.Log("Vader Not found");
                    return;
                    //previousCards.Add(cards[7]);
                    //previousCards.RemoveRange(0, 10);
                }
            }
        }
        else
        {
            int random = Random.Range(0, 101);
            //Debug.Log("Random: " + random);
            if (random <= 89)
            {
                int i = Random.Range(0, cards.Length - 1);
                selectedObject = cards[i];
                chosenSpawnClip = cardSpawnClips[i];
                previousCards.Add(selectedObject);
            }
            else
            {
                selectedObject = cards[7];
                chosenSpawnClip = cardSpawnClips[7];
                previousCards.Add(selectedObject);
            }
        }
    }

    void SpawnObjects()
    {
        if (type < 3)
        {
            selectedObject = types[type];
            if(selectedObject.name == "Yoda Coin")
            {
                chosenSpawnClip = yodaSpawnClip;
            }
            else
            {
                chosenSpawnClip = coinSpawnClip;
            }
        }
        else
        {
            PickNextCard();

        }
        
        if(startingObjectNums[type + 1] >= selectedStartNum)
        {
            Debug.Log("Too Many: " + startingObjectNums[type + 1] + " >= " + selectedStartNum);
            return;
        }
        else
        {
            spawnedObjects.Add(selectedObject);
            spawnedObjectClips.Add(chosenSpawnClip);
            StartCoroutine(SpawnWait());
        }
    }

    IEnumerator SpawnWait()
    {
        yield return new WaitForSeconds(3);
        if (spawning)
        {
            while (spawning)
            {
                yield return null;
            }
        }
        spawning = true;
        yield return new WaitForSeconds(Random.Range(5, 20));
        if (spawnedObjects.Count > 0 )
        {
            float lerpTime = 0f;
            float posX = Random.Range(minPosX, maxPosX);
            //Vector3 pos = new Vector3(posX, shooter.position.y, shooter.position.z);
            if (left)
            {
                while (shooter.position.x < posX)
                {
                    Vector3 pos = new Vector3(posX + .02f, 15.1f, 5.85f);
                    shooter.position = Vector3.Lerp(shooter.position, pos, lerpTime * Time.deltaTime);
                    lerpTime += spawnerSpeed;
                    yield return null;
                }
            }
            else
            {
                while (shooter.position.x > posX)
                {
                    Vector3 pos = new Vector3(posX - .02f, 15.1f, 5.85f);
                    shooter.position = Vector3.Lerp(shooter.position, pos, lerpTime * Time.deltaTime);
                    lerpTime += spawnerSpeed;
                    yield return null;
                }
            }
            yield return new WaitForSeconds(.5f);
            GameObject spawnedObj = Instantiate(spawnedObjects[0], shooterSpawn.position, Quaternion.identity, spawnParent);
            spawnedObj.name = spawnedObjects[0].name;
            //Debug.Log("SelectedObject: " + selectedObject.name);
            //Debug.Log("SpawnedObject: " + spawnedObjects[0].name);
            spawnSound.PlayOneShot(spawnedObjectClips[0]);
            spawnedObjectClips.RemoveAt(0);
            spawnedObjects.RemoveAt(0);
            
            yield return new WaitForSeconds(.5f);
            lerpTime = 0f;
            if (left)
            {
                while (shooter.position.x < shooterStartR.x - .02f)
                {
                    shooter.position = Vector3.Lerp(shooter.position, shooterStartR, lerpTime * Time.deltaTime);
                    lerpTime += spawnerSpeed;
                    yield return null;
                }
                left = false;
            }
            else
            {
                while (shooter.position.x > shooterStartL.x + .02f)
                {
                    shooter.position = Vector3.Lerp(shooter.position, shooterStartL, lerpTime * Time.deltaTime);
                    lerpTime += spawnerSpeed;
                    yield return null;
                }
                left = true;
            }
            //Debug.Log("Over");
            spawning = false;
        }
        yield break;
    }

    void CheckStartingAmounts()
    {
        for (int i = 0; i < startingObjectNums.Length; i++)
        {
            if (startingObjectNums[i] < startingObjectNumLimits[i])
            {
                selectedStartNum = startingObjectNumLimits[i];
                type = i - 1;
                for (int j = 0; j < startingObjectNumLimits[i] - startingObjectNums[i]; j++)
                {
                    SpawnObjects();
                    Debug.Log((startingObjectNumLimits[i] - startingObjectNums[i]) + ": " + i);
                }  
            }

        }
    }

    ///Gold Coin
    void AddGoldCoin()
    {
        if(startingObjectNums[5] < 1)
        {
            StartCoroutine(GoldCoinDelay());
        }
    }
    IEnumerator GoldCoinDelay()
    {
        while (goldCoinTime >= 0)
        {
            goldCoinTime -= Time.deltaTime;
            yield return null;
        }
        startingObjectNums[5]++;
        goldCoinTime = maxGoldCoinTime * 60;
        spawnedObjects.Add(goldCoinObject);
        spawnedObjectClips.Add(goldSpawnClip);
        StartCoroutine(SpawnWait());
    }

    void SetGoldCoinTime()
    {
        if (PlayerPrefs.GetInt("GoldCoinDrop", 0) == 1)
        {
            maxGoldCoinTime = 5;
        }
        else
        {
            maxGoldCoinTime = 10;
        }
        goldCoinTime = PlayerPrefs.GetFloat("GoldCoinTime", maxGoldCoinTime * 60);
    }
    public void GetGoldTimeHalf()
    {
        Debug.Log("GoldCoinTime: " + goldCoinTime);
        maxGoldCoinTime = 5;
        goldCoinTime /= 2;
        Debug.Log("GoldCoinTime: " + goldCoinTime);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("GoldCoinTime", goldCoinTime);
    }
}
