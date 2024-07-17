using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
{
    public event Action<Collider2D[]> OnDetectedCollider2D;
    protected Player player;
    protected Vector2 offset;
    private Collider2D[] detected;

    protected void HandleAttackAction()
    {
        offset = new Vector2(
                transform.position.x + (currentAttackData.HitBox.center.x * player.FacingDirection),
                transform.position.y + currentAttackData.HitBox.center.y
            );

        detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, data.DetectableLayers);

        if (detected.Length == 0) 
            return;   

        OnDetectedCollider2D?.Invoke(detected);        
    }

    /*protected override void Awake()
    {
        base.Awake();
        
    }*/

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        eventHandler.OnAttackAction += HandleAttackAction;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        eventHandler.OnAttackAction -= HandleAttackAction;
    }

    private void OnDrawGizmosSelected()
        {
            if (data == null)
                return;

            foreach (var item in data.GetAllAttackData())
            {
                if (!item.Debug)
                    continue;
                
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.position, item.HitBox.size);
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.center, item.HitBox.size);
            }
        }
}
