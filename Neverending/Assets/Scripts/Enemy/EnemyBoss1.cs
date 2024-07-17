using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss1 : Enemy
{
    void Start()
    {
        
    }

    void Update()
    {
        Attack();
    }

    public override void Attack()
    {
        animator.SetTrigger("Attack");
        // animator.
    }
}
