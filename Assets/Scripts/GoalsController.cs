using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalsController : MonoBehaviour
{
    [SerializeField]
    GameObject goalObject;
    Text goalText;
    Image goalImage;
    [SerializeField]
    Color needColor;
    [SerializeField]
    Color haveColor;

    [SerializeField]
    GoalScriptableObject[] goalScriptableObjects;
    [SerializeField]
    Transform content;

    List<GameObject> goals;
    int achievedGoals;
    bool allGoals;

    private void Awake()
    {
        goals = new List<GameObject>();
        SpawnGoalObjects();
        if(PlayerPrefs.GetInt("All Goals", 0) == 1)
        {
            allGoals = true;
        }
    }
    
    void SpawnGoalObjects()
    {
        foreach(GoalScriptableObject ro in goalScriptableObjects)
        {
            GameObject goal = Instantiate(goalObject, content);
            goal.GetComponentInChildren<Text>().text = ro.rewardText;
            Image pic = goal.transform.GetChild(1).GetComponent<Image>();
            goals.Add(goal);
            if(PlayerPrefs.GetInt(ro.name, 0) == 1)
            {
                pic.color = haveColor;
            }
            else
            {
                pic.color = needColor;
            }
        }
    }

    public void CheckGoals()
    {
        //Debug.Log("Checking Goals...");
        for(int i = 0; i < goals.Count; i++)
        {
            //Debug.Log("Goal: " + goalScriptableObjects[i].name);
            if (PlayerPrefs.GetInt(goalScriptableObjects[i].name, 0) == 1)
            {
                Image pic = goals[i].transform.GetChild(1).GetComponent<Image>();
                pic.color = haveColor;
                achievedGoals++;
            }
        }
        if (!allGoals)
        {
            if(achievedGoals == goals.Count - 1)
            {
                allGoals = true;
                RewardController.popUpList.Add("All Goals");
                Debug.Log("All Goals");
            }
            else
            {
                achievedGoals = 0;
            }
        }
    }
}
