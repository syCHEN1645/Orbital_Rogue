using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newWeaponData", menuName ="Data/Weapon Data/Basic Weapon Data", order = 0)]
public class WeaponDataSO : ScriptableObject
{
    public float[] movementSpeed;
    [field: SerializeField] public int NumberOfAttacks { get; private set; }

    [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

    public T GetData<T>()
    {
        return ComponentData.OfType<T>().FirstOrDefault();
    }

    public void AddData(ComponentData data)
    {
        if (ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
        return;

        ComponentData.Add(data);
    }

    /*[ContextMenu("Add Sprite Data")]
    private void AddSpriteData() => componentData.Add(new WeaponSpriteData());

    [ContextMenu("Add Action HitBox Data")]
    private void AddActionHitBoxData() => componentData.Add(new ActionHitBoxData());

    [ContextMenu("Update Component Names")]
    private void UpdateComponentNames() 
    {
        foreach (var item in componentData)
        {
            item.SetComponentName();
            item.SetAttackDataNames();
        }
    }

    [ContextMenu("Update Number of Attacks")]
    private void UpdateNumberOfAttacks() 
    {
        foreach (var item in componentData)
        {
            item.InitializeAttackData(NumberOfAttacks);
        }
    }*/
}
