using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushToSide : MonoBehaviour
{
    bool isPushing;
    float dir;
    [SerializeField]
    float velocity;
    Rigidbody rb;
    private void Awake()
    {
        if(gameObject.transform.localPosition.x < 1f)
        {
            dir = -1f;
        }
        else
        {
            dir = 1f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Card") ||
            other.CompareTag("Elaut") ||
            other.CompareTag("Red") ||
            other.CompareTag("Green"))
        {

            if(TryGetComponent<Rigidbody>(out rb))
            {
                rb = other.gameObject.GetComponent<Rigidbody>();
            } 
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Push(rb);
    }
    private void OnTriggerExit(Collider other)
    {
        rb = null;
    }
    void Push(Rigidbody rb)
    {
        rb.AddForce(Vector3.right * dir * velocity, ForceMode.Force);
    }
}
