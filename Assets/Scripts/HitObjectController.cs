using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjectController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    GameObject glassExplosion;
    //Transform target;
    [SerializeField]
    float throwY;
    [SerializeField]
    float throwZ;

    float rotX;
    float rotY;
    float rotZ;
    float rotSpeed;
    Quaternion rot;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rotX = Random.Range(20, 40f);
        rotY = Random.Range(20f, 40f);
        rotZ = Random.Range(5f, 10f);
        rot = Quaternion.Euler(rotX, rotY, rotZ);
        rotSpeed = Random.Range(3f, 7f);


        StartCoroutine(Rotate());
    }

    private void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Target").transform;
        //Vector3 power = new Vector3(transform.forward.x, transform.forward.y * throwY, transform.forward.z * throwZ);
        rb.AddForce(transform.forward * throwZ, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        //rb.MoveRotation(Quaternion.Lerp(rb.rotation, rot, rotSpeed * Time.fixedDeltaTime));
    }

    IEnumerator Rotate()
    {
        float x = Random.Range(-7f, 7f);
        float y = Random.Range(-7f, 7f);
        float z = Random.Range(-7f, 7f);
        while (true)
        {
            rb.MoveRotation(rot);
            rot = Quaternion.Euler(rot.eulerAngles.x + x * Time.fixedDeltaTime, rot.eulerAngles.y + y * Time.fixedDeltaTime, rot.eulerAngles.z + z );
            yield return null;
        }
    }
}
