using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    protected Weapon weapon;
    protected AnimationEventHandler eventHandler;
    protected bool isAttackActive;

    public virtual void Init()
    {

    }

    protected virtual void Awake()
    {
        weapon = GetComponent<Weapon>();
        eventHandler = GetComponentInChildren<AnimationEventHandler>();
    }

    protected virtual void Start()
    {
        if (weapon == null)
        {
            Debug.LogWarning("no weapon;");
        }
        weapon.OnEnter += HandleEnter;
        weapon.OnExit += HandleExit;
    }

    protected virtual void HandleEnter()
    {
        isAttackActive = true;
    }

    protected virtual void HandleExit()
    {
        isAttackActive = false;
    }

    protected virtual void OnDestroy()
    {
        weapon.OnEnter -= HandleEnter;
        weapon.OnExit -= HandleExit;
    }
}

public abstract class WeaponComponent<T1, T2> : WeaponComponent
    where T1 : ComponentData<T2> where T2 : AttackData
{
    protected T1 data;
    protected T2 currentAttackData;

    protected override void HandleEnter()
    {
        base.HandleEnter();

        currentAttackData = data.GetAttackData(weapon.CurrentAttackCounter);
    }

    public override void Init()
    {
        base.Init();

        data = weapon.Data.GetData<T1>();
    }
}
