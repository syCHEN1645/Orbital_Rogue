using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerControlScript;

public class Interact : MonoBehaviour
{
    public WeaponInventory primaryWeaponInventory;
    public WeaponInventory secondaryWeaponInventory;
    public static Action<WeaponDataSO> OnSwitchPrimaryWeapon;
    public static Action<WeaponDataSO> OnSwitchSecondaryWeapon;
    private PlayerControls playerControls;   
    public InteractableDetector InteractableDetector { get; private set; }
    private Weapon PrimaryWeapon;
    private Weapon SecondaryWeapon;
    private InputAction playerAction;
    private WeaponPickup weaponPickup;
   
    
    private void Awake() 
    {
        InteractableDetector = GameObject.Find("InteractableDetector").GetComponent<InteractableDetector>();
        PrimaryWeapon = GameObject.Find("PrimaryWeapon").GetComponent<Weapon>();
        SecondaryWeapon = GameObject.Find("SecondaryWeapon").GetComponent<Weapon>();
        playerControls = new PlayerControls();
    }

    private void OnEnable() 
    {
        playerControls?.Enable();
    }

    private void Disable() 
    {
        playerControls?.Disable();
    }

    private void Update()
    {
        if (playerControls.Player.PrimaryWeaponPickup.WasPressedThisFrame()) {
            HandleWeaponPickup(PrimaryWeapon, primaryWeaponInventory);            
        } else if (playerControls.Player.SecondaryWeaponPickup.WasPressedThisFrame()) {
            HandleWeaponPickup(SecondaryWeapon, secondaryWeaponInventory);            
        }
    }

    private void HandleWeaponPickup(Weapon weapon, WeaponInventory weaponInventory) {
        if (InteractableDetector.isDetected) {
            weaponPickup = InteractableDetector.weaponPickup;
            if (weaponPickup != null) {
                var currentWeaponData = weapon.Data;
                var newWeaponData = weaponPickup.GetContext();
                weapon.SetData(newWeaponData);
                weapon.GenerateData(newWeaponData);
                weaponPickup.SetContext(currentWeaponData);
                Debug.Log(weaponInventory.name);
                weaponInventory.SwitchWeapon(newWeaponData); 
            }
        }
    }
}