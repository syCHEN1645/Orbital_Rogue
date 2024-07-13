using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particalOnHitPrefabVFX;
    private DataPackage dataPackage;
    private Vector3 startPosition;
    private bool isDataSet;

    private void Start()
    {
        startPosition = transform.position;
        isDataSet = false;
    }

    private void Update()
    {
        MoveProjectile();
        if (isDataSet) {
            DetectFireDistance();
        }
    }

    public void SetProjectileData(DataPackage dataPackage)
    {
        this.dataPackage = dataPackage;
        isDataSet = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructable indestructable = other.gameObject.GetComponent<Indestructable>();
        
        if (!other.isTrigger && (enemyHealth || indestructable)) {
            enemyHealth?.TakeDamage(dataPackage.AttackDamage);
            Instantiate(particalOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void DetectFireDistance()
    {
        Debug.Log(dataPackage.Range);
        if (Vector3.Distance(transform.position, startPosition) > dataPackage.Range) {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
    
}
