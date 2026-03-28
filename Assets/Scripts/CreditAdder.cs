using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class CreditAdder : MonoBehaviour
{
    [SerializeField]
    GameObject confirmMenu;
    [SerializeField]
    GameObject loadScreen;
    [SerializeField]
    UnityEvent saveEvent;
    public void ClickAdder()
    {
        confirmMenu.SetActive(true);
    }

    public void ClickYes()
    {
        saveEvent.Invoke();
        loadScreen.SetActive(true);
        SceneManager.LoadSceneAsync("Kid Attack");
    }

    public void ClickNo()
    {
        confirmMenu?.SetActive(false);
    }
}
