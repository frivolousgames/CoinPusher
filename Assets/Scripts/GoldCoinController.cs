using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCoinController : MonoBehaviour
{
    public static bool isBonusTime;

    [SerializeField]
    GameObject goldMenu;
    [SerializeField]
    Image bonusObjectImage;
    [SerializeField]
    Text bonusMultiText;
    [SerializeField]
    GoldShotObject[] goldObjects;
    List<GoldShotObject> goldObjectsList;
    public static GoldShotObject chosenObject;
    public static int chosenObjectMulti;
    List<int> chosenIndexes;
    public static int chosenStartNum;

    [SerializeField]
    float bonusLength;
    [SerializeField]
    float bonusBlinkLength;

    [SerializeField]
    Animator goldAnim;
    [SerializeField]
    Animator bonusAnim;
    [SerializeField]
    bool isShooting;
    [SerializeField]
    bool isBlinking;

    [SerializeField]
    AudioSource bonusSong;

    private void Awake()
    {
        PlayerPrefs.SetInt("ElautShot", 1);
        PlayerPrefs.SetInt("ElautShotMulti", 2);
        PlayerPrefs.SetInt("TrooperShot", 1);
        //PlayerPrefs.SetInt("CardShot", 1); //TEMP
        //PlayerPrefs.SetInt("CardShotMulti", 2); //TEMP
        goldObjectsList = new List<GoldShotObject>();
        chosenIndexes = new List<int>();
    }

    private void Update()
    {
        goldAnim.SetBool("isShooting", isShooting);
        bonusAnim.SetBool("isBlinking", isBlinking);
    }

    public void StartBonus()
    {
        SetBonusObject();
        goldMenu.SetActive(true);
    }

    void SetBonusObject()
    {
        for(int i = 0; i < goldObjects.Length; i++)
        {
            Debug.Log(goldObjects.Length);
            if (PlayerPrefs.GetInt(goldObjects[i].objectName, 0) == 1)
            {
                Debug.Log("Object: " + goldObjects[i].name);
                goldObjectsList.Add(goldObjects[i]);
                chosenIndexes.Add(i);
            }
        }
        int j = Random.Range(0, goldObjectsList.Count);
        chosenObject = goldObjectsList[j];
        chosenStartNum = chosenIndexes[j];
        bonusObjectImage.sprite = goldObjectsList[j].objectPic;
        if(PlayerPrefs.GetInt(goldObjectsList[j].objectName + "Multi", 1) > 1)
        {
            bonusMultiText.text = "X" + PlayerPrefs.GetInt(goldObjectsList[j].objectName + "Multi", 1).ToString();
        }
        else
        {
            bonusMultiText.text = "";
        }
        chosenObjectMulti = PlayerPrefs.GetInt(goldObjectsList[j].objectName + "Multi", 1);
        Debug.Log("Multi: " + PlayerPrefs.GetInt("CardShotMulti", 1) + " " + chosenObjectMulti);
    }

    public void StartBonusTime()
    {
        StartCoroutine(BonusRoutine());
    }

    IEnumerator BonusRoutine()
    {
        isShooting = true;
        bonusSong.Play();
        yield return new WaitForSeconds(bonusLength);
        isBlinking = true;
        yield return new WaitForSeconds(bonusBlinkLength);
        bonusSong.Stop();
        isBlinking = false;
        isShooting = false;
        isBonusTime = false;
        goldObjectsList.Clear();
        goldMenu.SetActive(false);
        RewardController.isPopUp = false;
    }

}
