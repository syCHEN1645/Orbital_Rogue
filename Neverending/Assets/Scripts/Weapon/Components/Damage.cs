using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : WeaponComponent<DamageData, AttackDamage>
{
    private ActionHitBox hitBox;

    private void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if (item.CompareTag("Enemy"))
            {
                Debug.Log($"item: {item.tag} + {item.name}");
                item.GetComponent<EnemyHealth>().TakeDamage(currentAttackData.Amount);
            }
        }
    }

    public float GetWeaponDamage()
    {
        return currentAttackData.Amount;
    }

    protected override void Start()
    {
        base.Start();

        hitBox = GetComponent<ActionHitBox>();
            
        hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
    }  
}

