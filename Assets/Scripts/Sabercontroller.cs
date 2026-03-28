using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sabercontroller : MonoBehaviour
{
    [SerializeField]
    GameObject saberMenu;
    [SerializeField]
    GameObject saberPanel;

    public static bool saberEnd;
    [SerializeField]
    Animator panelAnim;

    private void Update()
    {
        panelAnim.SetBool("End", saberEnd);
    }
    public void SetActive()
    {
        saberMenu.SetActive(true);
        saberPanel.SetActive(true);
    }
}
