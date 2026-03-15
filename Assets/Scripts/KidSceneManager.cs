using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KidSceneManager : MonoBehaviour
{
    [SerializeField]
    GameObject reticle;
    [SerializeField]
    Camera cam;
    [SerializeField]
    float offset;

    [SerializeField]
    Transform spawnTrans;
    [SerializeField]
    GameObject[] projectiles;
    [SerializeField]
    float shootWait;
    bool isShooting;
    [SerializeField]
    Vector3 shootOffset;

    [SerializeField]
    Slider kidSlider;
    public static int kidHealth;
    public static int kidMaxHealth = 100;

    [SerializeField]
    Text timeText;
    float maxTime = 30f;
    float time;
    public static bool isLost;

    private void Awake()
    {
        Physics.gravity = new Vector3(0f, -90, 0f);
        kidHealth = kidMaxHealth;
        kidSlider.value = kidHealth;
        time = maxTime;
        timeText.text = Mathf.RoundToInt(time).ToString();
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = reticle.transform.position.z;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        mousePos.z = reticle.transform.position.z;
        reticle.transform.position = new Vector3(mousePos.x * offset, (mousePos.y * offset) - offset, reticle.transform.position.z);

        spawnTrans.LookAt(reticle.transform.position);
        kidSlider.value = kidHealth;

        if (KidStartPanel.isStarted && !KidController.isDead && !isLost)
        {
            time-= Time.deltaTime;
            timeText.text = Mathf.RoundToInt(time).ToString();
        }
    }

    public void Shoot()
    {
        Debug.Log("Shooting");
        if (!isShooting && !KidController.isDead && KidStartPanel.isStarted)
        {
            Debug.Log("Shooting");
            isShooting = true;
            StartCoroutine(ShootWait());
        }
    }

    IEnumerator ShootWait()
    {
        Instantiate(projectiles[Random.Range(0, projectiles.Length)], spawnTrans.position + shootOffset, spawnTrans.rotation);
        yield return new WaitForSeconds(shootWait);
        isShooting = false;
        yield break;
    }
}
