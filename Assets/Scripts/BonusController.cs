using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
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
    [SerializeField]
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

    private void Awake()
    {
        lightIndex = 0; //temp lightIndex = Gamecontroller.lightIndex
        isCounted = false; //temp isCounted = GameController.isCounted;
    }

    public void ActivateCountLights()
    {
        if(CoinShooter.isShooting)
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
                    SetSpinBonusValue();
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
    }

    void SetSpinBonusValue()
    {
        if(pastBonuses.Count < 10)
        {
            spinRandom = Random.Range(1, 101);
            Debug.Log("SpinRandom: " + spinRandom);
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
        }
        else
        {
            foreach (var bonus in pastBonuses)
            {
                if(bonus == 50)
                {
                    pastBonuses.Clear();
                    return;
                }
            }
            spinBonusValue = 50;
        }
        SelectBonusIndex();
        StartCoroutine(SpinLights());
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
        Debug.Log("spinBonusValue: " + spinBonusValue);
        Debug.Log("SelectedBonusIndex: " + selectedBonusIndex);
    }

    IEnumerator SpinLights()
    {

        yield return new WaitForSeconds(.5f);
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
        yield return new WaitForSeconds(.5f);
        int startSpin = 0;
        int index = 0;
        int lastIndex = 0;
        startSpinLength = 60 + selectedBonusIndex + 15;
        Debug.Log("startSpinLength: " + startSpinLength);
        while(startSpin < startSpinLength)
        {
            if(index == spinLights.Length)
            {
                index = 0;
            }
            if(index == 0)
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
            //Debug.Log("StartSpin: " + startSpin);
            yield return new WaitForSeconds(startSpinSpeed);
        }
    }
}
