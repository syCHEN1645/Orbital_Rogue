using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponDataSO Data { get; private set; }
    public int CurrentAttackCounter
        {
            get => currentAttackCounter;
            private set => currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;
        }

    public event Action OnEnter;
    public event Action OnExit;
    public event Action OnUseInput;

    protected Animator baseAnimator;
    public GameObject BaseGameObject { get; private set; }
    public GameObject WeaponSpriteGameObject { get; private set; }
    public SpriteRenderer BaseSpriteRenderer { get; private set; }
    public SpriteRenderer WeaponSpriteRenderer { get; private set; }
    public AnimationEventHandler EventHandler { get; private set; }
    public WeaponGenerator weaponGenerator { get; private set; }
    protected PlayerAttackState state;
    private int currentAttackCounter;

    protected void Awake()
    {
        BaseGameObject = transform.Find("Base").gameObject;
        WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;

        BaseSpriteRenderer = BaseGameObject.GetComponent<SpriteRenderer>();
        WeaponSpriteRenderer = WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
        
        baseAnimator = BaseGameObject.GetComponent<Animator>();

        EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();

        weaponGenerator = GetComponent<WeaponGenerator>();

        gameObject.SetActive(true);
    }

    public void Enter() 
    {
        gameObject.SetActive(true);

        baseAnimator.SetBool("Attack", true);

        baseAnimator.SetInteger("AttackCounter", CurrentAttackCounter);

        OnEnter?.Invoke();
    }

    public void SetData(WeaponDataSO data)
    {
        Data = data;

        //weaponGenerator.GenerateWeapon(data);
            
        if(Data is null)
            return;
            
        ResetAttackCounter();
    }

    public void GenerateData(WeaponDataSO data) 
    {
        weaponGenerator.GenerateWeapon(data);
    }

    private void ResetAttackCounter()
    {
        print("Reset Attack Counter");
        CurrentAttackCounter = 0;
    }

    public void WeaponFlip(bool value) 
    {
        BaseSpriteRenderer.flipX = value;
        WeaponSpriteRenderer.flipX = value;
    }

    public void Exit()
    {
        baseAnimator.SetBool("Attack", false);

        CurrentAttackCounter++;

        gameObject.SetActive(false);
    
        OnExit?.Invoke();
    }

    private void OnEnable() 
    {
        EventHandler.OnUseInput += HandleUseInput;
        EventHandler.OnFinish += Exit;
    }

    private void OnDisable() 
    {
        EventHandler.OnUseInput -= HandleUseInput;
        EventHandler.OnFinish -= Exit;
    }

    private void HandleUseInput() => OnUseInput?.Invoke();
    
    public void initializeWeapon(PlayerAttackState state) 
    {
        this.state = state;
    } 
}
