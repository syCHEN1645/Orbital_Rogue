using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public Weapon weapon;
    public GameObject item;
    private Image image;

    public void SwitchWeapon(WeaponDataSO weaponData)
    {
        image.sprite = weaponData.Icon;
    }

    private void Awake()
    {
        image = item.GetComponent<Image>();
        
    }

    private void Start()
    {
        image.sprite = weapon.Data.Icon;
    }
    
}
