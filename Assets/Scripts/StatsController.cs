using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{
    [SerializeField]
    Text points;
    [SerializeField]
    Text sets;
    [SerializeField]
    Text silver;
    [SerializeField]
    Text trooper;
    [SerializeField]
    Text vader;
    [SerializeField]
    Text yoda;
    [SerializeField]
    Text gold;
    [SerializeField]
    Text cards;
    [SerializeField]
    Text time;
    [SerializeField]
    Text kids;

    private void Start()
    {
        kids.text = PlayerPrefs.GetInt("Kids Killed", 0).ToString();
    }
    private void Update()
    {
        points.text = PlayerPrefs.GetInt("Total Points", 0).ToString();
        sets.text = PlayerPrefs.GetInt("Total Sets", 0).ToString();
        silver.text = PlayerPrefs.GetInt("Total Silver", 0).ToString();
        trooper.text = PlayerPrefs.GetInt("Total Trooper", 0).ToString();
        vader.text = PlayerPrefs.GetInt("Total Vader", 0).ToString();
        yoda.text = PlayerPrefs.GetInt("Total Yoda", 0).ToString();
        gold.text = PlayerPrefs.GetInt("Total Gold", 0).ToString();
        cards.text = PlayerPrefs.GetInt("Total Cards", 0).ToString();
        time.text = PlaySceneManager.timeFormatted;
    }
}
