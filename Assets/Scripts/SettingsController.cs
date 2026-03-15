using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    GameObject quitMenu;

    bool isPointerVolume;

    [SerializeField]
    Slider[] volumeSliders;
    [SerializeField]
    AudioMixer mixer;
    string[] names;
    string[] properties;

    Slider selectedSlider;
    string selectedName;
    string selectedProperty;

    GameObject[] coins;

    [SerializeField]
    GameObject purgeMenu;
    
    private void Awake()
    {
        names = new string[]
        {
            "FX", "Songs"
        };

        properties = new string[]
        {
            "FXVol", "SongVol"
        };
    }

    private void Start()
    {
        for (int i = 0; i < volumeSliders.Length; i++)
        {
            float v = PlayerPrefs.GetFloat(names[i], 0f);
            mixer.SetFloat(properties[i], v);
            volumeSliders[i].value = v;
            Debug.Log("Vol: " + v);
        }
    }
    public void QuitMenuYes()
    {
        Application.Quit();
    }
    public void QuitMenuNo()
    {
        quitMenu.SetActive(false);
    }

    public void OpenQuitMenu()
    {
        quitMenu.SetActive(true);
    }

    public void PointerDownVolumeSlider()
    {
        
        isPointerVolume = true;
        for (int i = 0; i < volumeSliders.Length; i++)
        {
            
            if (VolumeSlider.selectedSlider.name == volumeSliders[i].name)
            {
                selectedSlider = volumeSliders[i];
                selectedName = names[i];
                selectedProperty = properties[i];
                Debug.Log("SelectedSlider: " + volumeSliders[i].name);
                Debug.Log("Slider: " + VolumeSlider.selectedSlider.name);
                break;
            }
            else
            {
                
                
            }
        }
        StartCoroutine(SetVolume(selectedSlider, selectedName, selectedProperty));
    }

    public void PinterUpVolumeSlider()
    {
        isPointerVolume = false;
    }

    IEnumerator SetVolume(Slider slider, string name, string property)
    {
        while (isPointerVolume)
        {
            mixer.SetFloat(property, slider.value);
            Debug.Log("Vol: " + property);
            yield return null;
        }
        PlayerPrefs.SetFloat(name, slider.value);
        yield break;
    }

    public void PurgeCoins()
    {
        coins = GameObject.FindGameObjectsWithTag("ElautSave");
        int c = coins.Length;
        Debug.Log("Coins Pre: " + coins.Length);
        if(coins.Length > 600)
        {
            for(int i = c; i > 600; i--)
            {
                int j = Random.Range(0, coins.Length);
                Debug.Log("j: " + j);    
                Destroy(coins[j]);
            }
        }
        Debug.Log("Coins Post: " + coins.Length);
        purgeMenu.SetActive(false);
    }

    public void OpenPurgeMenu()
    {
        purgeMenu.SetActive(true);
    }
    public void ClosePurgeMenu()
    {
        purgeMenu.SetActive(false);
    }

}
