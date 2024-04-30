using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Light,
    Heavy
}
public class DamageSource : MonoBehaviour
{
    [SerializeField] private int LightAttackDamageAmount = 1;
    [SerializeField] private int HeavyAttackDamageAmount = 3;
    private AttackType currentAttackType;

    public void SetAttackType(AttackType attackType)
    {
        currentAttackType = attackType;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();

            switch (currentAttackType)
            {
                case AttackType.Light:
                    enemyHealth.TakeDamage(LightAttackDamageAmount);
                    break;
                case AttackType.Heavy:
                    enemyHealth.TakeDamage(HeavyAttackDamageAmount);
                    break;
                default:
                    break;
            }
        }
    }


}
