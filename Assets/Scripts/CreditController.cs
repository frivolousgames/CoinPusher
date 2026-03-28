using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditController : MonoBehaviour
{
    Text creditText;

    private void Awake()
    {
        creditText = GetComponent<Text>();
        creditText.text = "$" + PlaySceneManager.credits.ToString() + ".00";
    }

    private void Update()
    {
        creditText.text = "$" + PlaySceneManager.credits.ToString() + ".00";
    }
}
