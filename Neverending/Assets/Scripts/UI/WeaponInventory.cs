using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    [field: SerializeField] private Weapon weapon;
    [field: SerializeField] private GameObject item;
    private Image image;

    public void SwitchWeapon(WeaponDataSO weaponData)
    {
        image.sprite = weaponData.Icon;
    }

    private void Awake()
    {
        image = item.GetComponent<Image>();
        //weapon = transform.Find()
    }
    private void Start()
    {
        image.sprite = weapon.Data.Icon;
    }
}
