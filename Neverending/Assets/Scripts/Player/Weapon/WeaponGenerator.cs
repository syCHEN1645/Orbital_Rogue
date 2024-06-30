using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private WeaponDataSO data;

    private List<WeaponComponent> componentAlreadyOnWeapon = new List<WeaponComponent>();

    private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();

    private List<Type> componentDependencies = new List<Type>();

    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        GenerateWeapon(data);
    }

    [ContextMenu("Test Generate")]
    private void TestGenerate()
    {GenerateWeapon(data);}

    public void GenerateWeapon(WeaponDataSO data)
    {
        weapon.SetData(data);

        componentAlreadyOnWeapon.Clear();
        componentsAddedToWeapon.Clear();
        componentDependencies.Clear();

        componentAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();

        componentDependencies = data.GetAllDependencies();

        foreach (var dependency in componentDependencies)
        {
            if (componentsAddedToWeapon.FirstOrDefault(componentsAddedToWeapon => componentAlreadyOnWeapon.GetType() == dependency))
                continue;

            var WeaponComponent = componentAlreadyOnWeapon.FirstOrDefault(componentAlreadyOnWeapon => componentAlreadyOnWeapon.GetType() == dependency);

            if (WeaponComponent == null)
            {
                WeaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
            }

            WeaponComponent.Init();

            componentsAddedToWeapon.Add(WeaponComponent);
        }

        var componentsToRemove = componentAlreadyOnWeapon.Except(componentsAddedToWeapon);

        foreach (var WeaponComponent in componentsToRemove)
        {
            Destroy(WeaponComponent);
        }

        animator.runtimeAnimatorController = data.AnimatorController;
    }
}