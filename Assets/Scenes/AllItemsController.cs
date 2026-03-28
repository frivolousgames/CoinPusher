using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllItemsController : MonoBehaviour
{
    [SerializeField]
    GameObject allItemsMenu;

    public void SetActive()
    {
        allItemsMenu.SetActive(true);

    }
    public void ClickOk()
    {
        allItemsMenu.SetActive(false);
    }
}
