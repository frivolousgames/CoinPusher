using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinShooter : MonoBehaviour
{
    [SerializeField]
    Transform coinSpawn;
    [SerializeField]
    GameObject coin;
    Vector3 coinRot;
    float shootWait;
    public static bool isShooting;
    bool hasPlays;
    //[SerializeField]
    //Transform coinParent;

    ///BONUS//////////////
    [SerializeField]
    Text playsText;
    [SerializeField]
    Text bonusText;

    [SerializeField]
    GameObject[] countLightsL;
    [SerializeField]
    GameObject[] countLightsR;
    [SerializeField]
    GameObject topLight;
    [SerializeField]
    GameObject bottomLight;
    [SerializeField]
    GameObject[] spinLights;

    int lightIndex;
    bool isCounted;

    int startSpinLength;
    float startSpinSpeed;

    int[] spinLightValues =
    {
      50,5,5,15,15,5,5,20,20,5,5,10,10,5,5,50,5,5,15,15,5,5,20,20,5,5,10,10,5,5,
      50,5,5,15,15,5,5,20,20,5,5,10,10,5,5,50,5,5,15,15,5,5,20,20,5,5,10,10,5,5
    };
    int spinRandom;
    int spinBonusValue;
    List<int> pastBonuses = new List<int>();

    List<int> selectedBonusIndexes = new List<int>();
    int selectedBonusIndex;

    ///AUDIO////////
    [SerializeField]
    AudioSource coinShootSound;
    [SerializeField]
    AudioSource bonusSong;
    [SerializeField]
    AudioSource bonusArp;
    [SerializeField]
    AudioSource bonusAdder;
    [SerializeField]
    AudioSource coinShootReal;
    [SerializeField]
    AudioClip coinShootRealClip1;
    [SerializeField]
    AudioClip coinShootRealClip2;
    [SerializeField]
    AudioClip coinShootRealClip3;
    [SerializeField]
    AudioClip coinShootRealClip4;
    List<AudioClip> coinClips;

    private void Awake()
    {
        isShooting = false;
        shootWait = .4f;
        hasPlays = true; //temp
        coinRot = new Vector3 (0f, 0f, 90f);
        

        //Bonus//////
        lightIndex = 0; //temp lightIndex = Gamecontroller.lightIndex
        isCounted = false; //temp isCounted = GameController.isCounted;

        //Audio///
        coinClips = new List<AudioClip>();
        coinClips.Add (coinShootRealClip1);
        coinClips.Add(coinShootRealClip2);
        coinClips.Add(coinShootRealClip3);
        coinClips.Add(coinShootRealClip4);
    }
    private void Update()
    {
        //hasPlays = GameManager.hasPlays;
        //credits = GameManagerDependencyInfo.credits
        
    }

    public void Shoot()
    {
        if(!GoldCoinController.isBonusTime)
        {
            if (!isShooting && PlaySceneManager.plays > 0)
            {
                isShooting = true;
                StartCoroutine(ShootRoutine());
            }
        }
        else
        {
            if(!isShooting)
            {
                isShooting = true;
                StartCoroutine(BonusShootRoutine());
            }
        }
    }

    IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(shootWait);
        GameObject c = Instantiate(coin, coinSpawn.position, Quaternion.Euler(coinRot), transform);
        c.name = "Coin";
        ActivateCountLights();
        PlaySceneManager.plays--;
        coinShootSound.Play();
        int tempCoinIndex = Random.Range(0, coinClips.Count);
        coinShootReal.pitch = Random.Range(.9f, 1.1f);
        coinShootReal.PlayOneShot(coinClips[tempCoinIndex]);
        isShooting = false;
        yield break;
    }

    ///BONUS///////

    void ActivateCountLights()
    {
        if (!isCounted)
        {
            if (!bottomLight.activeInHierarchy)
            {
                bottomLight.SetActive(true);
            }
            else if (lightIndex == countLightsR.Length)
            {
                topLight.SetActive(true);
                isCounted = true;
                lightIndex = 0;
                StartCoroutine(PauseBeforeSpin());
                return;
            }
            else
            {
                countLightsL[lightIndex].SetActive(true);
                countLightsR[lightIndex].SetActive(true);
                lightIndex++;
            }
        }
        else
        {
            lightIndex++;
        }
    }

    IEnumerator PauseBeforeSpin()
    {
        while(spinBonusValue > 0)
        {
            yield return null;
        }
        yield return new WaitForSeconds(.3f);
        SetSpinBonusValue();
        yield break;
    }

    void ActivateCountLightsAfterBonus()
    {
        if(lightIndex > 0)
        {
            bottomLight.SetActive(true); 
            for(int i = 0; i < lightIndex; i++)
            {
                countLightsL[i].SetActive(true);
                countLightsR[i].SetActive(true);
            }
        }
        
    }
    void SetSpinBonusValue()
    {
        if (pastBonuses.Count < 10)
        {
            spinRandom = Random.Range(1, 101);
            //Debug.Log("SpinRandom: " + spinRandom);
            if (spinRandom <= 50)
            {
                spinBonusValue = 5;
            }
            else if (spinRandom > 50 && spinRandom <= 70)
            {
                spinBonusValue = 10;
            }
            else if (spinRandom > 70 && spinRandom <= 85)
            {
                spinBonusValue = 15;
            }
            else if (spinRandom > 85 && spinRandom <= 95)
            {
                spinBonusValue = 20;
            }
            else
            {
                spinBonusValue = 50;
            }
            pastBonuses.Add(spinBonusValue);
            SelectBonusIndex();
        }
        else
        {
            foreach (var bonus in pastBonuses)
            {
                if (bonus == 50)
                {
                    pastBonuses.Clear();
                    SetSpinBonusValue();
                    return;
                }
            }
            spinBonusValue = 50;
            pastBonuses.Clear();
            SelectBonusIndex();
        }
    }

    void SelectBonusIndex()
    {
        for (int i = 0; i < spinLightValues.Length; i++)
        {
            if (spinLightValues[i] == spinBonusValue)
            {
                //Debug.Log("SpinLightValueIndex: " + i);
                selectedBonusIndexes.Add(i);
            }
        }
        int tempBonusIndex = Random.Range(0, selectedBonusIndexes.Count);
        selectedBonusIndex = selectedBonusIndexes[tempBonusIndex];
        StartCoroutine(SpinLights());
        //Debug.Log("spinBonusValue: " + spinBonusValue);
        //Debug.Log("SelectedBonusIndex: " + selectedBonusIndex);
    }



    IEnumerator SpinLights()
    {
        foreach (var light in countLightsL)
        {
            light.SetActive(false);
        }
        foreach (var light in countLightsR)
        {
            light.SetActive(false);
        }
        topLight.SetActive(false);
        bottomLight.SetActive(false);
        yield return new WaitForSeconds(.2f);
        bonusSong.Play();
        int startSpin = 0;
        int index = 0;
        int lastIndex = 0;
        startSpinLength = 60 + selectedBonusIndex + 15;
        //Debug.Log("startSpinLength: " + startSpinLength);
        startSpinSpeed = .01f;
        while (startSpin <= startSpinLength)
        {
            if (index == spinLights.Length)
            {
                index = 0;
            }
            if (index == 0)
            {
                lastIndex = spinLights.Length - 1;
            }
            else
            {
                lastIndex = index - 1;
            }
            spinLights[index].SetActive(true);
            spinLights[lastIndex].SetActive(false);
            startSpin++;
            index++;
            if(startSpinLength - startSpin < 16)
            {
                startSpinSpeed += .02f;
            }
            //Debug.Log("StartSpin: " + startSpin);
            yield return new WaitForSeconds(startSpinSpeed);
        }
        if(bonusSong.isPlaying)
        {
            bonusSong.Stop();
        }
        //Debug.Log("Index: " + index);
        int tempBonus = 0;
        int tempBonusSum = 0;
        if(int.TryParse(bonusText.text, out tempBonus))
        {
            tempBonusSum = tempBonus + spinBonusValue;
            bonusText.text = tempBonusSum.ToString();
        }
        else
        {
            bonusText.text = spinBonusValue.ToString();
        }
        bonusArp.Play();
        while (bonusArp.isPlaying)
        {
            if(spinLights[index - 1].activeInHierarchy)
            {
                spinLights[index - 1].SetActive(false);
            }
            else
            {
                spinLights[index - 1].SetActive(true);
            }
            yield return new WaitForSeconds(.2f);
        }
        spinLights[index - 1].SetActive(false);
        //yield return new WaitForSeconds(.3f);
        selectedBonusIndexes.Clear();
        ActivateCountLightsAfterBonus();
        isCounted = false;
        while (spinBonusValue > 0)
        {
            PlaySceneManager.plays++;
            spinBonusValue--;
            bonusText.text = spinBonusValue.ToString();
            bonusAdder.Play();
            yield return new WaitForSeconds(.1f);
        }
        yield break;
    }

    ///BONUS SHOOT
    IEnumerator BonusShootRoutine()
    {
        yield return new WaitForSeconds(shootWait);
        for(int i = 0; i < GoldCoinController.chosenObjectMulti; i++)
        {
            int j = Random.Range(0, GoldCoinController.chosenObject.objectGO.Length);
            GameObject fab = Instantiate(GoldCoinController.chosenObject.objectGO[j], coinSpawn.position, Quaternion.Euler(coinRot), transform);
            fab.name = GoldCoinController.chosenObject.objectGO[j].name;
            CollectController.startingObjectNums[GoldCoinController.chosenStartNum]++;
            Debug.Log("StartNum: " + CollectController.startingObjectNums[GoldCoinController.chosenStartNum]);
        }
        //Instantiate(coin, coinSpawn.position, Quaternion.Euler(coinRot), transform);
        ActivateCountLights();
        //PlaySceneManager.credits--;
        coinShootSound.Play();
        int tempCoinIndex = Random.Range(0, coinClips.Count);
        coinShootReal.pitch = Random.Range(.9f, 1.1f);
        coinShootReal.PlayOneShot(coinClips[tempCoinIndex]);
        isShooting = false;
        yield break;
    }
}
