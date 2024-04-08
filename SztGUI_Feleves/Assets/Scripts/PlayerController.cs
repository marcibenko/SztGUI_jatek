using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    //sprite animalashoz
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;


    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
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
            //ha a heavyt itt hivom meg akkor se jo
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
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void LightAttack()
    {
        myAnimator.SetTrigger("LightAttack");
        //myAnimator.ResetTrigger("LightAttack");
    }

    private void HeavyAttack()
    {
        //valami a heavy atacckal rossz
        myAnimator.SetTrigger("HeavyAttack");
        //myAnimator.ResetTrigger("HeavyAttack");
        
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
        }
    }



    IEnumerator Dash()
    {
        moveSpeed = 10f;
        yield return new WaitForSeconds(0.2f);
        moveSpeed = 1f;
       // yield return new WaitForSeconds(0.2f);
    }
}
