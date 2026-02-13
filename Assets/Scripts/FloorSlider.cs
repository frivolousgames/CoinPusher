using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorSlider : MonoBehaviour
{
    Rigidbody rb;

    Vector3 startPos;
    Vector3 endPos;
    [SerializeField]
    float slideMin;
    [SerializeField]
    float slideMax;

    [SerializeField]
    float slideSpeed;

    public static bool isSliding;

    [SerializeField]
    GameObject startCol;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isSliding = true;
        startPos = transform.position;
        endPos = new Vector3(transform.position.x, transform.position.y, slideMax);
    }
    private void Start()
    {
        StartCoroutine(Slide());
        StartCoroutine(StartColDisable());
    }

    IEnumerator Slide()
    {
        float time = 0f; 
        while(true)
        {
            while(isSliding)
            {
                float z = Mathf.SmoothStep(slideMin, slideMax, Mathf.PingPong(time * slideSpeed, 1));
                rb.MovePosition(new Vector3(transform.position.x, transform.position.y, z));
                //rb.velocity = Vector3.Lerp(startPos, endPos, Mathf.PingPong(Time.time * slideSpeed, 1));
                time += Time.deltaTime;
                yield return null;
            }
            yield return null;
        }
    }

    IEnumerator StartColDisable()
    {
        yield return new WaitForSeconds(1f);
        startCol.SetActive(false);
        yield break;
    }
}
