using System.Collections;
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
    bool isWon;

    [SerializeField]
    GameObject endScreen;

    [SerializeField]
    float[] credits;
    public static float selectedCredits;

    float timePlayed;

    //Audio
    [SerializeField]
    GameObject themePlayer;

    private void Awake()
    {
        Physics.gravity = new Vector3(0f, -90, 0f);
        kidHealth = kidMaxHealth;
        kidSlider.value = kidHealth;
        time = maxTime;
        timeText.text = Mathf.RoundToInt(time).ToString();
        isLost = false;
    }

    private void Start()
    {
        timePlayed = PlayerPrefs.GetFloat("Total Time", 0f);
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
            reticle.SetActive(true);
            Cursor.visible = false;
        }
        if (Mathf.RoundToInt(time) == 0)
        {
            if(!isLost && !KidController.isDead)
            {
                isLost = true;
                reticle.SetActive(false);
                Cursor.visible = true;
                themePlayer.SetActive(false);
                endScreen.SetActive(true);
            }
        }
        if(KidController.isDead)
        {
            if (!isWon)
            {
                isWon = true;
                reticle.SetActive(false);
                Cursor.visible = true;
                themePlayer.SetActive(false);
                SetCreditAmount();
                //float tempCredits = PlayerPrefs.GetFloat("Credits", 0f);
                PlayerPrefs.SetFloat("Credits", selectedCredits);
                endScreen.SetActive(true);
                //stats
                int i = PlayerPrefs.GetInt("Kids Killed", 0);
                PlayerPrefs.SetInt("Kids Killed", i + 1);
            }
        }

        //stats
        timePlayed += Time.deltaTime;
        PlayerPrefs.SetFloat("Total Time", timePlayed);
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

    void SetCreditAmount()
    {
        int rand = Random.Range(0, 101);
        if(rand >= 0 && rand <= 25)
        {
            selectedCredits = credits[0];
        }
        else if (rand > 25 && rand <= 70)
        {
            selectedCredits = credits[1];
        }
        else if (rand > 70 && rand <= 90)
        {
            selectedCredits = credits[2];
        }
        else
        {
            selectedCredits = credits[3];
        }
        Debug.Log("Rand: " + rand);
    }
}
