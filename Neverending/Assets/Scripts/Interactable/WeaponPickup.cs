using System;
using UnityEngine;
using UnityEngine.Serialization;


    [RequireComponent(typeof(Rigidbody2D))]
    public class WeaponPickup : MonoBehaviour, IInteractable<WeaponDataSO>
    {
        [field: SerializeField] public WeaponDataSO weaponData { get; private set; }

        private Rigidbody2D Rigidbody2D;
        private SpriteRenderer weaponIcon;
        private Bobber bobber;
        
        public WeaponDataSO GetContext() => weaponData;
        public void SetContext(WeaponDataSO context)
        {
            weaponData = context;

            weaponIcon.sprite = weaponData.Icon;
        }

        public void Interact()
        {
            Destroy(gameObject);
        }

        public void EnableInteraction()
        {
            bobber.StartBobbing();
        }

        public void DisableInteraction()
        {
            bobber.StopBobbing();
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            weaponIcon = GetComponent<SpriteRenderer>();
            
            if(weaponData != null) {
                weaponIcon.sprite = weaponData.Icon;
            }
        }
    }
