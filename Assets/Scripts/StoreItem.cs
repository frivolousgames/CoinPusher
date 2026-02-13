using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    public static StoreItem selectedItem;
    bool selected;
    Image bg;
    Image itemImage;

    public Text itemNameText;
    public Text itemPricetext;
    [SerializeField]
    Color normalColor;
    [SerializeField]
    Color clickedColor;

    private void OnEnable()
    {
        bg = GetComponent<Image>();
        if(selectedItem == null)
        {
            selected = true;
            selectedItem = this;
            SetSelectedProps();
        }
        //Debug.Log(selectedItem.name);
    }

    private void OnDisable()
    {
        selectedItem = null;
    }

    public void SetSelectedItem()
    {
        if(selectedItem != this)
        {
            selectedItem = this;
            selected = true;
            SetSelectedProps();
        }
    }

    void SetSelectedProps()
    {
        bg.color = clickedColor;
        itemPricetext.color = normalColor;
        itemNameText.color = normalColor;
    }

    void ResetSelectedProps()
    {
        bg.color = normalColor;
        itemPricetext.color = clickedColor;
        itemNameText.color = clickedColor;
    }

    private void Update()
    {
        if (selectedItem != this)
        {
            if(selected)
            {
                selected = false;
                ResetSelectedProps();
            }
        }
    }
}
