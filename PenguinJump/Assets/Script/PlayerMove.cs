using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Variables")]
    Rigidbody2D rigid;
    Animator animator;
    private Vector2 inputVector;
    public float moveSpeed;
    public float jumpDistance;
    public float DamagedForce;
    [SerializeField] bool isFacingctrl;

    [Header("Ground Check")]
    [SerializeField] bool isGrounded;
    public Transform groundSpot;
    public LayerMask groundLayer;

    [Header("Dash Variables")]
    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] float dashPower;
    [SerializeField] float dashTime;
    [SerializeField] float dashCooldown;
    [SerializeField] float dashGravity;
    private float normalGravity;
    private float waitTime;

    [Header("Wall Variables")]
    [SerializeField] bool isWall;
    [SerializeField] bool isWallJump;
    public Transform wallChk;
    public float wallChkDistance;
    public LayerMask wall_Layer;
    public float slidingSpeed;
    float isRight = 1;


    [SerializeField] bool isDamaged = false;
    [SerializeField] float maxSpeed = 20f;
    public ParticleSystem damagedparticle;

    //public GameObject Damagedeffect;

    public void Start()
    {
        //GameObject damagedeffect = Instantiate(Damagedeffect);
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //damagedparticle = GetComponent<ParticleSystem>();

        normalGravity = rigid.gravityScale;
    }

    public void Awake()
    {
        isFacingctrl = true;
        canDash = true;
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if (!isDamaged)
        {
            rigid.velocity = new Vector2(inputVector.x * moveSpeed, rigid.velocity.y);
        }

        if(isWall)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * slidingSpeed);

            
        }





        //Max Speed
        if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        if (rigid.velocity.y > maxSpeed)
            rigid.velocity = new Vector2(rigid.velocity.x, maxSpeed);

        //Debug.Log("현재힘 : " + rigid.velocity.magnitude);
    }

    void Update()
    {
        waitTime += Time.deltaTime;
        if (isDashing)
        {
            return;
        }
        isGrounded = Physics2D.OverlapCircle(groundSpot.position, 0.2f, groundLayer);

        //캐릭터 보는 방향
        CheckDirection();

        //벽점프
        isWall = Physics2D.Raycast(wallChk.position, Vector2.right * isRight, wallChkDistance, wall_Layer);

        //animator

        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            animator.SetBool("walk", false);
        }
        else if (Mathf.Abs(rigid.velocity.y) == 0 && !animator.GetBool("jumpup") && !animator.GetBool("jumpdown"))
        {
            animator.SetBool("walk", true);
        }

        if (isGrounded)
        {
            animator.SetBool("jumpdown", false);
            animator.SetBool("jumpup", false);
        }
        else if (!isGrounded)
        {
            if (Mathf.Abs(rigid.velocity.y) > 1.5)
            {
                animator.SetBool("jumpup", true);
            }
            else if (Mathf.Abs(rigid.velocity.y) < 4 && Mathf.Abs(rigid.velocity.y) > 1)
            {
                animator.SetBool("jumpdown", true);
            }
            else
            {
                animator.SetBool("jumpup", false);
                animator.SetBool("jumpdown", false);
            }
        }

        if (isDamaged)
        {
            animator.SetBool("isGround", false);
            if (isGrounded && isDamaged)
            {
                animator.SetBool("isGround", true);
                if (!damagedparticle.isPlaying)
                {
                    DamagedEffectPlay();
                }
                Invoke("OffDamaged", 2);
            }
        }

        if (isWall)
        {
            animator.SetBool("isSliding", true);
        }else if (!isWall)
        {
            animator.SetBool("isSliding", false);
        }

        //animator.SetBool("isSliding", isWall);



        /*
         if (Mathf.Abs(rigid.velocity.y) > 1.5)
            {
                animator.SetBool("jumpup", true);
            }
            else if (Mathf.Abs(rigid.velocity.y) < 4 && Mathf.Abs(rigid.velocity.y) > 1)
            {
                animator.SetBool("jumpdown", true);
            }
            else
            {
                animator.SetBool("jumpup", false);
                animator.SetBool("jumpdown", false);
            }
        */
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && isGrounded && !isDamaged)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpDistance);
        }

        if (isWall)
        {
            isDamaged = false;
            if (context.performed)
            {
                isDamaged = true;
                Invoke("Frozen", 0.45f);
                rigid.velocity = new Vector2(-isRight * jumpDistance, 0.9f * jumpDistance);
                Flip();
            }
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if(context.performed && canDash && !isDamaged)
        {
            //StartCoroutine(Dash());
            
            if(waitTime >= dashCooldown)
            {
                waitTime = 0;
                Invoke("Dash", 0);

            }
            
        }
    }
    /*
    IEnumerator Dash()
    {
        // http://www.youtube.com/watch?v=2kFgmuPHiA0&t=122s
        canDash = false;
        isDashing = true;
        float originalGravity = rigid.gravityScale;
        rigid.gravityScale = 0;
        rigid.velocity = new Vector2(transform.localScale.x * dashPower, 0);
        dashTrail.emitting = true;
        yield return new WaitForSeconds(dashTime);
        dashTrail.emitting = false;
        rigid.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    */

    void CheckDirection()
    {
        if(inputVector.x > 0 && !isFacingctrl && !isDamaged)
        {
            Flip();
        }
        else if(inputVector.x < 0 && isFacingctrl && !isDamaged)
        {
            Flip();
        }
    }

    void Flip()
    {
        isRight *= -1;
        isFacingctrl = !isFacingctrl;
        transform.Rotate(new Vector3(0, 180, 0));
    }

    public void Dash()
    {
        canDash = false;
        isDashing = true;
        rigid.gravityScale = dashGravity;
        animator.SetTrigger("Dash");

        if (inputVector.x == 0)
        {
            if(isFacingctrl)
            {
                rigid.velocity = new Vector2(transform.localScale.x * dashPower, 0);
            }
            if (!isFacingctrl)
            {
                rigid.velocity = new Vector2(-transform.localScale.x * dashPower, 0);
            }
        }
        else
        {
            rigid.velocity = new Vector2(inputVector.x * dashPower, 0);
        }
        Invoke("StopDash", dashTime);
    }

    public void StopDash()
    {
        canDash = true;
        isDashing = false;
        rigid.gravityScale = normalGravity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            OnDamaged(collision.transform.position);
            Debug.Log("적이랑만남");
        }
    }
    /*
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OnDamaged(collision.transform.position);
            Debug.Log("적이랑만남 Trigger");
        }
    }
    */
    public void OnDamaged(Vector2 targetPos)
    {
        isDamaged = true;

        int dirc = transform.position.y - targetPos.y > 0 ? 1 : -1;

        rigid.AddForce(new Vector2(-1, dirc) * DamagedForce, ForceMode2D.Impulse);

        animator.SetBool("damaged", true);
        animator.SetTrigger("Damaged");
       
    }

    public void OffDamaged()
    {
        if (isGrounded)
        {
            isDamaged = false;
        }
    }

    public void DamagedEffectPlay()
    {
        damagedparticle.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(wallChk.position, Vector2.right * isRight * wallChkDistance);
    }

    void Frozen()
    {
        isDamaged = false;
    }
}
