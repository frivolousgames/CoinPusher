using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KidController : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;

    [SerializeField]
    float jumpY;
    [SerializeField]
    float jumpXSide;
    [SerializeField]
    float jumpYSide;

    [SerializeField]
    bool isHit;
    [SerializeField]
    bool isWalking;
    [SerializeField]
    bool isJumpUp;
    [SerializeField]
    bool isLand;
    [SerializeField]
    bool isJumpSide;
    [SerializeField]
    bool jumpLeft;
    [SerializeField]
    bool jumpRight;
    [SerializeField]
    bool isLicking;

    public static bool isDead;
    [SerializeField]
    bool isOver;
    [SerializeField]
    bool isLose;
    [SerializeField]
    Transform loseTrans;

    bool isLeft;
    bool isRight;

    [SerializeField]
    float walkSpeed;
    float walkDirection;
    [SerializeField]
    float rotSpeed;
    [SerializeField]
    float runSpeed;
    float moveSpeed;

    [SerializeField]
    Transform targetR;
    [SerializeField]
    Transform targetL;
    Transform[] targets;
    Transform selectedTarget;

    [SerializeField]
    float walkLength;
    [SerializeField]
    float jumpUpLength;
    [SerializeField]
    float lickLength;
    [SerializeField]
    float jumpSideLength;
    [SerializeField]
    float idleTime;

    bool isTurning;

    //[SerializeField]
    //Transform frontTrans;
    //[SerializeField]
    //Transform backTrans;

    Coroutine kidCoroutine;
    [SerializeField]
    GameObject reticle;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        walkDirection = 1;
        isDead = false;
        reticle.SetActive(false);
        moveSpeed = walkSpeed;
    }

    private void Start()
    {
        targets = new Transform[]
        {
            targetR, targetL
        };
    }
    private void Update()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isLicking", isLicking);
        anim.SetBool("isJumpUp", isJumpUp);
        anim.SetBool("isLand", isLand);
        anim.SetBool("isJumpSide", isJumpSide);
        anim.SetBool("jumpLeft", jumpLeft);
        anim.SetBool("jumpRight", jumpRight);
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isTurning", isTurning);
        anim.SetBool("isDead", isDead);
        anim.SetBool("isOver", isOver);
        anim.SetBool("isLose", isLose);

        //Debug.Log("IsWalking: " + isWalking);

        if (isDead)
        {
            isWalking = false;
            isJumpSide = false;
            isJumpUp = false;
            isLicking = false;
            jumpRight = false;
            jumpLeft = false;
            isTurning = false;
            if(kidCoroutine != null)
            {
                StopCoroutine(kidCoroutine);
            }
        }
        if (!isLose)
        {
            if (KidSceneManager.isLost)
            {
                isLose = true;
                moveSpeed = runSpeed;
                StopCoroutine(kidCoroutine);
                StartCoroutine(LoseRoutine());
            }
        }
    }

    private void FixedUpdate()
    {
        Walk(moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isLand = true;
            Debug.Log("Landed");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isLand = false;
            Debug.Log("InAir");
        }
    }


    IEnumerator KidRoutine()
    {
        while (!isDead)
        {
            Coroutine jumpUpRoutine = StartCoroutine(JumpUpRoutine());
            yield return new WaitForSeconds(jumpUpLength);
            isJumpUp = false;

            while (!isLand && !isDead)
            {
                yield return null;
            }
            yield return new WaitForSeconds(idleTime);

            Coroutine walkRoutine = StartCoroutine(WalkRoutine());
            yield return new WaitForSeconds(walkLength);
            isWalking = false;
            yield return new WaitForSeconds(idleTime);

            StartCoroutine(TurnRoutine());
            while (isTurning && !isDead)
            {
                yield return null;
            }

            Coroutine jumpSideRoutine = StartCoroutine(JumpSideRoutine());
            yield return new WaitForSeconds(jumpSideLength);
            isJumpSide = false;
            yield return new WaitForSeconds(idleTime);
            
            while (!isLand && !isDead)
            {
                yield return null;
            }

            Coroutine lickRoutine = StartCoroutine(LickRoutine());
            yield return new WaitForSeconds(lickLength);
            isLicking = false;
            yield return new WaitForSeconds(idleTime);
        }
        yield break;
    }

    IEnumerator WalkRoutine()
    {
        isWalking = true;
        int i = SetDirection();
        selectedTarget = targets[i];
        while (isWalking && !isDead)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            if (i == 0)
            {
                i = 1;
            }
            else
            {
                i = 0;
            }
            selectedTarget = targets[i];
            yield return null;
        }
        Debug.Log("Exit WalkRoutine");
        yield break;
    }

    IEnumerator JumpSideRoutine()
    {
        isJumpSide = true;
        int i = SetDirection();
        if(i == 0)
        {
            jumpLeft = true;
        }
        else
        {
            jumpLeft = false;
        }
        transform.rotation = Quaternion.identity;
        while(isJumpSide && !isDead)
        {
            
            while (isLand && !isDead)
            {
                yield return null;
            }
            while (!isLand && !isDead)
            {
                yield return null;
            }
            if (isLand && !isDead)
            {
                if (i == 0)
                {
                    i = 1;
                    jumpLeft = false;
                }
                else
                {
                    i = 0;
                    jumpLeft = true;
                }
            }
            yield return null;
        }
        Debug.Log("Exit JumpSideRoutine");
        yield break;
    }

    IEnumerator JumpUpRoutine()
    {
        isJumpUp = true;
        transform.rotation = Quaternion.identity;
        Debug.Log("Exit JumpUpRoutine");
        yield break;
    }

    IEnumerator LickRoutine()
    { 
        isLicking = true;
        Debug.Log("Exit LickRoutine");
        yield break;
    }

    IEnumerator TurnRoutine()
    {
        isTurning = true;
        while (transform.rotation != Quaternion.identity && !isDead)
        {
            if(Mathf.Abs(transform.rotation.eulerAngles.y - Quaternion.identity.eulerAngles.y) < .1f)
            {
                transform.rotation = Quaternion.identity;
                Debug.Log(transform.rotation);
            }

            rb.MoveRotation(Quaternion.Lerp(rb.rotation, Quaternion.identity, 10 * Time.fixedDeltaTime));
            yield return null;
        }
        isTurning = false;
        Debug.Log("Exit TurnRoutine");
        yield break;
    }

    IEnumerator LoseRoutine()
    {
        //while (!isLand)
        //{
        //    yield return null;
        //}
        isJumpSide = false;
        isJumpUp = false;
        isLicking = false;
        jumpRight = false;
        jumpLeft = false;
        isTurning = false;
        yield return new WaitForSeconds(.5f);
        isWalking = true;
        selectedTarget = loseTrans;
    }

    public void JumpUp()
    {
        if (isLand && !isDead)
        {
            rb.AddForce(Vector3.up * jumpY, ForceMode.Impulse);
        } 
    }

    public void JumpSide()
    {
        if (isLand && !isDead)
        {
            if (jumpLeft)
            {
                rb.AddForce(new Vector3(jumpXSide, jumpYSide, 0f), ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(new Vector3(-jumpXSide, jumpYSide, 0f), ForceMode.Impulse);
            }
        }
    }

    void Walk(float moveSpeed)
    {
        if (isWalking && !isDead)
        {
            Vector3 targetPos = new Vector3(selectedTarget.position.x, transform.position.y, transform.position.z);
            Vector3 rot = new Vector3(targetPos.x, 0f, 0f);
            Quaternion lookRot = Quaternion.LookRotation(-rot, Vector3.up);
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, lookRot, rotSpeed * Time.fixedDeltaTime));
            rb.MovePosition(Vector3.Lerp(transform.position, targetPos,  moveSpeed * Time.fixedDeltaTime));
        }
    }

    int SetDirection()
    {
        float distance = 0;
        int dir = 0;
        for (int j = 0; j < targets.Length; j++)
        {
            if (Vector3.Distance(transform.position, targets[j].position) > distance)
            {
                distance = Vector3.Distance(transform.position, targets[j].position);
                selectedTarget = targets[j];
                dir = j;
            }
        }
        return dir;
    }

    public void Hit()
    {
        if (HitCollision.hitFront)
        {
            anim.SetTrigger("hitBackward");
        }
        else
        {
            anim.SetTrigger("hitForward");
        }
    }

    public void StartRoutine()
    {
        kidCoroutine = StartCoroutine(KidRoutine());
        reticle.SetActive(true);
    }

    public void SetIsOver()
    {
        isOver = true;
    }
}
