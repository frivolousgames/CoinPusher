using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullSetController : MonoBehaviour
{
    [SerializeField]
    GameObject fullSetPanel;

    int points;
    [SerializeField]
    Text pointsText;

    [SerializeField]
    float drainWait;

    bool isDraining;
    [SerializeField]
    bool isOver;

    [SerializeField]
    Animator anim;

    ///Audio///
    [SerializeField]
    AudioSource drainSound;

    private void Awake()
    {
        points = 5000;
        isDraining = false;
        isOver = false;
    }

    private void Start()
    {
        pointsText.text = "+" + points.ToString();
    }

    private void Update()
    {
        anim.SetBool("isOver", isOver);
    }
    public void SetActive()
    {
        fullSetPanel.SetActive(true);
        points = 5000;
        pointsText.text = "+" + points.ToString();
    }

    public void CollectPoints()
    {
        if (!isDraining && points > 0)
        {
            isDraining = true;
            StartCoroutine(DrainPoints());
        }
    }

    IEnumerator DrainPoints()
    {
        while (points > 0)
        {
            SceneManager.score += 10;
            points -= 10;
            pointsText.text = "+" + points.ToString();
            drainSound.Play();
            yield return new WaitForSeconds(drainWait);
        }
        yield return new WaitForSeconds(.5f);
        isOver = true;
        isDraining = false;
        yield return new WaitForSeconds(1f);
        isOver = false;
        RewardController.isPopUp = false;
        yield break;
    }
}
