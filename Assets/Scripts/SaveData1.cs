using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData1 : MonoBehaviour
{
    public void SaveObject(GameObject go, string type)
    {
        if(type == "Card")
        {
            int a = PlayerPrefs.GetInt("Save" + type + go.name, 0);
            PlayerPrefs.SetFloat("Save" + type + go.name + a + "x", go.transform.position.x);
            PlayerPrefs.SetFloat("Save" + type + go.name + a + "y", go.transform.position.y);
            PlayerPrefs.SetFloat("Save" + type + go.name + a + "z", go.transform.position.z);
            PlayerPrefs.SetInt("Save" + type + go.name, a + 1);
            int k = PlayerPrefs.GetInt(type + "StartNum", 0);
            PlayerPrefs.SetInt(type + "StartNum", k + 1);
        }
        else
        {
            int a = PlayerPrefs.GetInt(type, 0);
            PlayerPrefs.SetFloat(type + a + "x", go.transform.position.x);
            PlayerPrefs.SetFloat(type + a + "y", go.transform.position.y);
            PlayerPrefs.SetFloat(type + a + "z", go.transform.position.z);
            PlayerPrefs.SetInt(type, a + 1);
            int k = PlayerPrefs.GetInt(type + "StartNum", 0);
            PlayerPrefs.SetInt(type + "StartNum", k + 1);
        }
    }

    public void LoadObject(GameObject prefab, GameObject[] cardPrefabs, string type, Transform parent)
    {
        if (type == "Card")
        {
            PlayerPrefs.SetInt(type + "StartNum", 0);
            for (int i = 0; i < 8; i++)
            {
                int a = PlayerPrefs.GetInt("Save" + type + i, 0);
                if(a > 0)
                {
                    for(int j = 0; j < a; j++)
                    {
                        GameObject c = Instantiate(cardPrefabs[i], new Vector3(PlayerPrefs.GetFloat("Save" + type + i + j + "x"), PlayerPrefs.GetFloat("Save" + type + i + j + "y"), PlayerPrefs.GetFloat("Save" + type + i + j + "z")), Quaternion.identity, parent);
                        c.name = cardPrefabs[i].name;
                        if(c.transform.position.z < -4f || c.transform.position.y < 8.35f)
                        {
                            c.transform.position = new Vector3(Random.Range(-7.5f, 10f), Random.Range(8.75f, 9.3f), Random.Range(-3.5f, 2.5f));
                            Debug.Log("New Pos");
                        }
                    }
                }
                PlayerPrefs.SetInt("Save" + type + i, 0);
            }
            //int a = PlayerPrefs.GetInt("Save" + type, 0);
        }
        else
        {
            PlayerPrefs.SetInt(type + "StartNum", 0);
            int a = PlayerPrefs.GetInt(type, 0);
            for (int i = 0; i < a; i++)
            {
                GameObject o = Instantiate(prefab, new Vector3(PlayerPrefs.GetFloat(type + i + "x"), PlayerPrefs.GetFloat(type + i + "y"), PlayerPrefs.GetFloat(type + i + "z")), Quaternion.identity, parent);
                o.name = prefab.name;
                if (o.transform.position.z < -4f || o.transform.position.y < 8.35f)
                {
                    o.transform.position = new Vector3(Random.Range(-7.5f, 10f), Random.Range(8.75f, 9.3f), Random.Range(-3.5f, 2.5f));
                    Debug.Log("New Pos");
                }
            }
            PlayerPrefs.SetInt(type, 0);
        }
        
    }
}
