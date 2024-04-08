using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject lightAttackPrefab;
    [SerializeField] private GameObject heavyAttackPrefab;
    [SerializeField] private Transform attackSpawnPoint;

    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
        // Subscribe to light attack and heavy attack actions
        playerControls.Combat.LightAttack.started += _ => LightAttack();
        playerControls.Combat.HeavyAttack.started += _ => HeavyAttack();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void LightAttack()
    {
        myAnimator.SetTrigger("LightAttack");
        Instantiate(lightAttackPrefab, attackSpawnPoint.position, Quaternion.identity);
    }

    private void HeavyAttack()
    {
        myAnimator.SetTrigger("HeavyAttack");
        Instantiate(heavyAttackPrefab, attackSpawnPoint.position, Quaternion.identity);
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
