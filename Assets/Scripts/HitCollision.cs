using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitCollision : MonoBehaviour
{
    public static bool hitFront;
    public static bool isHit;
    [SerializeField]
    Transform transF;
    [SerializeField]
    Transform transB;
    float hitResetWait;

    [SerializeField]
    UnityEvent hitEvent;

    [SerializeField]
    GameObject glassExplosion;

    Vector3 eOffset;
    [SerializeField]
    float zOff;

    [SerializeField]
    int damage;

    private void Awake()
    {
        hitResetWait = 0.25f;
        eOffset = new Vector3(0f, 0f, zOff);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HitObject"))
        {
            if(!isHit)
            {
                isHit = true;
                StartCoroutine(ResetIsHit());
                Vector3 hitPoint = other.ClosestPointOnBounds(transform.position);
                if (Vector3.Distance(hitPoint, transF.position) < Vector3.Distance(hitPoint, transB.position))
                {
                    hitFront = true;
                }
                else
                {
                    hitFront = false;
                }
                hitEvent.Invoke();
                Destroy(other.gameObject);
                if(damage < KidSceneManager.kidHealth)
                {
                    KidSceneManager.kidHealth -= damage;
                }
                else
                {
                    KidSceneManager.kidHealth = 0;
                    KidController.isDead = true;
                }
                Instantiate(glassExplosion, hitPoint + eOffset, Quaternion.identity);
            }
        }
    }

    IEnumerator ResetIsHit()
    {
        yield return new WaitForSeconds(hitResetWait);
        isHit = false;
        yield break;
    }
}
