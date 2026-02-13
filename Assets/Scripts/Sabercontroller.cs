using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sabercontroller : MonoBehaviour
{
    [SerializeField]
    GameObject saberMenu;
    [SerializeField]
    GameObject saberPanel;

    public void SetActive()
    {
        saberMenu.SetActive(true);
        saberPanel.SetActive(true);
    }
}
