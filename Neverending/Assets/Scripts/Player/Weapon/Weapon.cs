using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [field: SerializeField] public WeaponDataSO Data { get; private set; }
    [SerializeField] private int numberOfAttacks;
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
    public  AnimationEventHandler eventHandler { get; private set; }
    protected PlayerAttackState state;
    private int currentAttackCounter;

    protected void Awake()
    {
        BaseGameObject = transform.Find("Base").gameObject;

        baseAnimator = BaseGameObject.GetComponent<Animator>();

        eventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();

        gameObject.SetActive(true);
    }

    public void Enter() 
    {
        gameObject.SetActive(true);

        baseAnimator.SetBool("Attack", true);

        baseAnimator.SetInteger("AttackCounter", CurrentAttackCounter);

        OnEnter?.Invoke();
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
        eventHandler.OnUseInput += HandleUseInput;
        eventHandler.OnFinish += Exit;
    }

    private void OnDisable() 
    {
        eventHandler.OnUseInput -= HandleUseInput;
        eventHandler.OnFinish -= Exit;
    }

    private void HandleUseInput() => OnUseInput?.Invoke();
    
    public void initializeWeapon(PlayerAttackState state) 
    {
        this.state = state;
    }

    /*
    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        state.SetPlayerVelocity(Data.movementSpeed[CurrentAttackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        state.SetPlayerVelocity(0f);
    } 

    public virtual void AnimationTurnOffFlipTrigger()
    {
        state.SetFlipCheck(false);
    }

    public virtual void AnimationTurnOnFlipTrigger()
    {
        state.SetFlipCheck(true);
    }
    */
    
    
}
