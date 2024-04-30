using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float moveSpeed = 2f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Knockback knockback;

    //sprite animalashoz
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;


    //weapon collision
    [SerializeField] private Transform WeaponCollider;
    private DamageSource WeaponColliderController;

    //for other scripts
    public static PlayerController instance;


    protected override void Awake()
    {
        base.Awake();
        instance = this;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Dash());
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            LightAttack();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            HeavyAttack();
        }
        
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        if (knockback.GettingKnockedBack) { return; }
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void LightAttack()
    {
        myAnimator.SetTrigger("LightAttack");
        WeaponColliderController.SetAttackType(AttackType.Light);
        //myAnimator.ResetTrigger("LightAttack");
    }

    private void HeavyAttack()
    {
        myAnimator.SetTrigger("HeavyAttack");
        WeaponColliderController.SetAttackType(AttackType.Heavy);
        //myAnimator.ResetTrigger("HeavyAttack");
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            WeaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            mySpriteRenderer.flipX = false;
            WeaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void TakeDamage()
    {
        myAnimator.SetTrigger("TakeDamage");
    }

    public void Death()
    {
        myAnimator.SetTrigger("Death");
    }

    IEnumerator Dash()
    {
        float prevmoveSpeed = moveSpeed;
        moveSpeed = 10f;
        //play anim
        //myAnimator.runtimeAnimatorController = OverrideController;
        myAnimator.SetTrigger("Roll");
        //Invoke("SwitchToOriginalController", OverrideController["Roll"].length);
        yield return new WaitForSeconds(0.2f);
        moveSpeed = prevmoveSpeed;
        yield return new WaitForSeconds(0.4f);
    }
}
