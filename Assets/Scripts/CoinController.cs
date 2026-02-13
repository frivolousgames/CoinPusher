using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float velocityZ;
    Vector3 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        if(transform.parent != null)
        {
            if (transform.parent.name == "Coin Shooter")
            {
                rb.AddForce(transform.parent.up * velocityZ, ForceMode.Impulse);
                transform.parent = null;
                transform.localScale = Vector3.one;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        
    }
    
}
