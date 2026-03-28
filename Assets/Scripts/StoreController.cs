using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    [SerializeField]
    Transform content;
    [SerializeField]
    Text pointsText;

    ///Scriptable Objects
    [SerializeField]
    StoreItemObject[] storeItemObjects;
    int[] itemPrices;
    public static String chosenObject;

    ///Store Item///
    [SerializeField]
    GameObject storeItemPrefab;
    public static Image staticChosenImage;
    [SerializeField]
    Image chosenImage;
    [SerializeField]
    Text chosenPriceText;
    int chosenPrice;
    [SerializeField]
    float xPos;
    float yPos;
    [SerializeField]
    float yOffset;
    [SerializeField]
    ScrollRect scrollRect;

    ///Not Available///
    [SerializeField]
    GameObject itemBlock;
    [SerializeField]
    GameObject buyButtonBlock;
    bool isPurchased;
    bool isTooMuch;

    ///BuyMenu
    [SerializeField]
    GameObject buyMenu;
    [SerializeField]
    Text priceSubtractor;
    [SerializeField]
    Transform mainCanvas;
    [SerializeField]
    Transform pointsTrans;

    [SerializeField]
    GameObject rewardPopup;

    ///GALLERY
    [SerializeField]
    Color needColor;
    [SerializeField]
    Color haveColor;
    [SerializeField]
    Image galleryImage;
    List<Image> galleryImages;

    [SerializeField]
    Transform galleryContent;

    [SerializeField]
    UnityEvent checkGoals;

    //BONUS
    bool isBonus;
    bool isMulti;
    string[] bonusTypes;
    string[] multiTypes;
    string chosenBonus;
    

    //SABER
    bool isSaber;
    int[] sabers;
    string[] saberTypes;
    int chosenSaber;
    string chosenSaberType;
    [SerializeField]
    UnityEvent saberEvent;

    //GOLD HALF TIME
    bool isGoldHalfTime;
    [SerializeField]
    UnityEvent goldHalfTimeEvent;

    private void Awake()
    {
        galleryImages = new List<Image>();
        itemPrices = new int[storeItemObjects.Length];
        sabers = new int[] {1, 3};
        saberTypes = new string[] { "Saber Sweep", "Saber Sweep X3" };
        multiTypes = new string[] { "Vader Coin Bonus Shot X2", "Yoda Coin Bonus Shot X2", "Card Bonus Shot X2" };
        bonusTypes = new string[] { "VaderShot", "YodaShot", "CardShot" };
        CreatePricesArray();
        SortItems();
        CreateItems();
    }
    private void Start()
    {
        scrollRect.Rebuild(CanvasUpdate.PostLayout);
        SetItemVariables();
    }

    private void Update()
    {
        SetItemVariables();
        pointsText.text = PlaySceneManager.score.ToString();
    }

    void CreateItems()
    {
        //xPos = 32.4f;
        //yPos = -12f; //-142
        //yOffset = -130f;
        for (int i = 0; i < itemPrices.Length; i++)// change to itemPrices when fixed
        {
            //Vector3 newPos = new Vector3(xPos, yPos, 0f);
            GameObject prefab = Instantiate(storeItemPrefab, content);
            prefab.name = storeItemObjects[i].name;
            //prefab.transform.localPosition = newPos;
            prefab.GetComponent<StoreItem>().itemNameText.text = storeItemObjects[i].itemName;
            prefab.GetComponent<StoreItem>().itemPricetext.text = storeItemObjects[i].itemPrice.ToString();
            //yPos += yOffset;

            //GALLERY
            Image galPrefab = Instantiate(galleryImage, galleryContent);
            galPrefab.name = storeItemObjects[i].name;
            galPrefab.sprite = storeItemObjects[i].itemImage;
            galleryImages.Add(galPrefab);
            if (PlayerPrefs.GetInt(storeItemObjects[i].name, 0) == 1)
            {
                galPrefab.color = haveColor;
            }
            else
            {
                galPrefab.color = needColor;
            }
        }
    }

    void SetItemVariables()
    {
        if(chosenObject != StoreItem.selectedItem.name)
        {
            //Debug.Log("Setting Variables");
            chosenObject = StoreItem.selectedItem.name;
            for (int i = 0; i < itemPrices.Length; i++)
            {
                if(StoreItem.selectedItem.name == storeItemObjects[i].name)
                {
                    chosenImage.sprite = storeItemObjects[i].itemImage;
                    chosenPrice = storeItemObjects[i].itemPrice;
                    chosenPriceText.text = chosenPrice.ToString();

                    //Bonus
                    if (storeItemObjects[i].isBonus)
                    {
                        isBonus = true;
                    }
                    else
                    {
                        isBonus = false;
                    }
                    if (storeItemObjects[i].isBonusMulti)
                    {
                        isMulti = true;
                        for(int k = 0; k < bonusTypes.Length; k++)
                        {
                            if (multiTypes[k] == storeItemObjects[i].itemName)
                            {
                                chosenBonus = bonusTypes[k];
                                break;
                            }
                        }
                    }
                    else
                    {
                        isMulti = false;

                    }
                    //Saber
                    if (storeItemObjects[i].isSaber)
                    {
                        isSaber = true;
                        for(int j = 0; j < sabers.Length; j++)
                        {
                            if (storeItemObjects[i].itemName == saberTypes[j])
                            {
                                chosenSaber = sabers[j];
                                chosenSaberType = "Sabers";
                                break;
                            }
                        }
                    }
                    else
                    {
                        isSaber = false;
                    }
                    //Gold
                    if (storeItemObjects[i].isGoldHalfTime)
                    {
                        isGoldHalfTime = true;
                    }
                    else
                    {
                        isGoldHalfTime = false;
                    }

                    if (PlayerPrefs.GetInt(chosenObject, 0) == 1)
                    {
                        if(isSaber)
                        {

                        }
                        else
                        {
                            itemBlock.SetActive(true);
                            buyButtonBlock.SetActive(true);
                        }
                    }
                    else if (chosenPrice > PlaySceneManager.score)
                    {
                        itemBlock.SetActive(false);
                        buyButtonBlock.SetActive(true);
                    }
                    else if (isMulti && PlayerPrefs.GetInt(chosenBonus, 0) < 1)
                    {
                        itemBlock.SetActive(false);
                        buyButtonBlock.SetActive(true);
                        //Debug.Log("Chosen Bonus: " + chosenBonus);
                    }

                    else
                    {
                        itemBlock.SetActive(false);
                        buyButtonBlock.SetActive(false);
                    }
                    return;
                }
            }
        }
    }

    void CreatePricesArray()
    {
        for(int i = 0; i < itemPrices.Length; i++)
        {
            itemPrices[i] = storeItemObjects[i].itemPrice;
        }
    }

    void SortItems()
    {
        Array.Sort(itemPrices, storeItemObjects);
    }

    public void OpenBuyMenu()
    {
        if (!buyMenu.activeInHierarchy)
        {
            if(!buyButtonBlock.activeInHierarchy)
            {
                buyMenu.SetActive(true);
            } 
        }
    }

    public void ConfirmBuy()
    {
        if (isMulti)
        {
            PlayerPrefs.SetInt(chosenBonus, 2);
            PlayerPrefs.SetInt(chosenObject, 1);
            Debug.Log("Chosen Object: " + chosenObject);
            Debug.Log("Chosen Bonus: " + chosenBonus);

        }
        else if (isSaber)
        {
            PlayerPrefs.SetInt(chosenObject, 1);
            int s = PlayerPrefs.GetInt(chosenSaberType, 0);
            PlayerPrefs.SetInt(chosenSaberType, s + chosenSaber);
            saberEvent.Invoke();
            PlaySceneManager.score -= chosenPrice;
            Text pS = Instantiate(priceSubtractor, mainCanvas);
            pS.transform.position = pointsTrans.position;
            pS.text = "-" + chosenPrice;
            buyMenu.SetActive(false);
            if (chosenPrice > PlaySceneManager.score)
            {
                itemBlock.SetActive(false);
                buyButtonBlock.SetActive(true);
            }
            SetGalleryImageColor();
            CheckForAllItems();
            return;
        }
        else
        {
            PlayerPrefs.SetInt(chosenObject, 1);
        }
        if (isGoldHalfTime)
        {
            goldHalfTimeEvent.Invoke();
        }

        Debug.Log("Bought");
        PlaySceneManager.score -= chosenPrice;
        Text ps = Instantiate(priceSubtractor, mainCanvas);
        ps.transform.position = pointsTrans.position;
        ps.text = "-" + chosenPrice;
        buyMenu.SetActive(false);
        buyButtonBlock.SetActive(true);
        itemBlock.SetActive(true);
        SetGalleryImageColor();
        CheckForAllItems();
    }
    public void CancelBuy()
    {
        buyMenu.SetActive(false);
    }

    void CheckForAllItems()
    {
        if(PlayerPrefs.GetInt("BuyAll", 0) == 0)
        {
            foreach (StoreItemObject so in storeItemObjects)
            {
                if (PlayerPrefs.GetInt(so.name, 0) == 0)
                {
                    return;
                }
            }
            PlayerPrefs.SetInt("BuyAll", 1);
            rewardPopup.SetActive(true);
            checkGoals.Invoke();
        }
    }

    //GALLERY
    void SetGalleryImageColor()
    {
        foreach(Image i in galleryImages)
        {
            if(i.name == chosenObject)
            {
                i.color = haveColor;
            }
        }
    }

    //SABER
    void CheckSaber()
    {

    }
}
