using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoinShooterTurner : MonoBehaviour
{
    [SerializeField]
    Transform coinShooter;
    public float minTurnY;
    public float maxTurnY;
    [SerializeField]
    float rotSpeed;
    public static bool isTurning;
    public static bool isTurnable;
    public static bool turningLeft;
    bool isPressed;
    Coroutine turnRoutine;

    [SerializeField]
    Image turnR;
    [SerializeField]
    Image turnL;

    [SerializeField]
    Sprite turnUp;
    [SerializeField]
    Sprite turnDown;


    ///Audio///
    [SerializeField]
    AudioSource turnSound;


    private void Awake()
    {
        isTurnable = true;
    }

    private void Start()
    {
        turnR.sprite = turnUp;
        turnL.sprite = turnUp;
    }

    private void Update()
    {
        
    }

    void TurningLeft()
    {
        if (gameObject.name == "L")
        {
            turningLeft = true;
            turnL.sprite = turnDown;
        }
        else
        {
            turningLeft = false;
            turnR.sprite = turnDown;
        }
    }

    void ButtonPressed()
    {
        isPressed = true;
        isTurning = true;
    }

    public void ButtonReleased()
    {
        isPressed = false;
        isTurning = false;
        turnR.sprite = turnUp;
        turnL.sprite = turnUp;
        turnSound.Stop();
        StopCoroutine(turnRoutine);
    }
    public void TurnShooter()
    {
        if(isTurnable)
        {
            TurningLeft();
            ButtonPressed();
            turnRoutine = StartCoroutine(Turn());
        }
    }

    IEnumerator Turn()
    {
        while(isPressed)
        {
            if(gameObject.name == "L" && turningLeft)
            {
                while(coinShooter.rotation.y > minTurnY)
                {
                    coinShooter.Rotate(0f, 0f, - rotSpeed * Time.deltaTime);
                    if (!turnSound.isPlaying)
                    {
                        turnSound.Play();
                    }
                    //Debug.Log("Left: " + coinShooter.rotation);
                    yield return null;
                }
                turnSound.Stop();
                yield return null;
            }
            else if(gameObject.name == "R" && !turningLeft)
            {
                while (coinShooter.rotation.y < maxTurnY)
                {
                    coinShooter.Rotate(0f, 0f, rotSpeed * Time.deltaTime);
                    if (!turnSound.isPlaying)
                    {
                        turnSound.Play();
                    }
                    //Debug.Log("Right: " + coinShooter.rotation);
                    yield return null;
                }
                turnSound.Stop();
                yield return null;
            }
            yield return null;
        }

        yield break;
    }
    
}
