using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
{
    private SpriteRenderer baseSpriteRenderer;
    private SpriteRenderer weaponSpriteRenderer;

    private int currentWeaponSpriteIndex;

    protected override void HandleEnter()
    {
        base.HandleEnter();

        currentWeaponSpriteIndex = 0;
    }

    private void HandleBaseSpriteChange(SpriteRenderer sr) 
    {
        if (!isAttackActive)
        {
            weaponSpriteRenderer.sprite = null;
            return;
        }

        var currentAttackSprites = currentAttackData.Sprites;

        if (currentWeaponSpriteIndex >= currentAttackSprites.Length)
        {
            Debug.LogWarning($"{weapon.name} weapon sprites length mismatch");
            return;
        }

        weaponSpriteRenderer.sprite = currentAttackSprites[currentWeaponSpriteIndex];
        
        currentWeaponSpriteIndex++;
    }

    protected override void Awake()
    {
        base.Awake();

        baseSpriteRenderer = transform.Find("Base").GetComponent<SpriteRenderer>();
        weaponSpriteRenderer = transform.Find("Weapon").GetComponent<SpriteRenderer>();

        data = weapon.Data.GetData<WeaponSpriteData>();

        //baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
        //weaponSpriteRenderer = weapon.WeaponGameObject.GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();

        baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);

        weapon.OnEnter += HandleEnter;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
        
        weapon.OnEnter -= HandleEnter;
    }
}
