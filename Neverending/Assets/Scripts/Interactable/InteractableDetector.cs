using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableDetector : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    public Canvas indicator;
    public WeaponPickup weaponPickup { get; private set; }
    public bool isDetected { get; private set; } 

    /*void Awake()
    {
        //player = GetComponentInParent<Player>();
        //PrimaryWeapon = GameObject.Find("PrimaryWeapon").GetComponent<Weapon>();
        //SecondaryWeapon = GameObject.Find("SecondaryWeapon").GetComponent<Weapon>();
        isDetected = false;
    }*/

    void Start()
    {
        indicator.transform.position = gameObject.transform.position;
        indicator.gameObject.SetActive(false);
        isDetected = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Interactable")) {
            if (other.name == "WeaponPickup") {
                weaponPickup = other.GetComponent<WeaponPickup>();
                Debug.Log(weaponPickup.GetType() + " Enter");
                //newData = weaponPickup.GetContext();
            } /*else if (other.name == "WeaponUpgrade") {
                playerData.Upgrade();
                //interactable = other.GetComponent<WeaponUpgrade>();
            }*/
            indicator.gameObject.SetActive(true);
            isDetected = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Interactable")) {
            //Debug.Log(weaponPickup.GetType() + " Exit");
            isDetected = false;
            indicator.gameObject.SetActive(false);
        }
    }
}
