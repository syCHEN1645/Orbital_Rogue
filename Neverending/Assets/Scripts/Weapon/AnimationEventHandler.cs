using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public event Action OnFinish;
    public event Action OnUseInput;    
    public event Action OnStartMovement;
    public event Action OnStopMovement;
    public event Action OnAttackAction;
    //public event Action<bool> OnFlipSetActive; 
    public event Action OnProjectileSpawn;
    public event Action<AttackPhases> OnEnterAttackPhase;

    private void UseInputTrigger() => OnUseInput?.Invoke();
    private void AnimationFinishedTrigger() => OnFinish?.Invoke();
    private void StartMovementTrigger() => OnStartMovement?.Invoke();
    private void StopMovementTrigger() => OnStopMovement?.Invoke();
    private void AttackActionTrigger() => OnAttackAction?.Invoke();
    //private void SetFlipActive() => OnFlipSetActive?.Invoke(true);
    //private void SetFlipInactive() => OnFlipSetActive?.Invoke(false);
    private void ProjectileSpawnTrigger() => OnProjectileSpawn?.Invoke();
    private void EnterAttackPhases(AttackPhases phase) => OnEnterAttackPhase?.Invoke(phase);
}

