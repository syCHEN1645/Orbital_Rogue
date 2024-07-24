using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedProjectile : MonoBehaviour
{
    [field: SerializeField] private float moveSpeed = 22f;
    [field: SerializeField] private GameObject particalOnHitPrefabVFX;
    private DataPackage dataPackage;
    private Vector3 startPosition;
    private bool isDataSet;
    private GameObject player;
    private PlayerData playerData;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerData = player.GetComponent<PlayerData>();
    }

    private void Start()
    {
        startPosition = transform.position;
        //isDataSet = false;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject collider = other.gameObject;
        Quaternion rot = Quaternion.Euler(0, 0, 0);
        EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
        if (collider.CompareTag("Enemy") || collider.CompareTag("Indestructable")) {
            //Debug.Log("check");
            enemyHealth?.TakeDamage(dataPackage.AttackDamage);
            Instantiate(particalOnHitPrefabVFX, transform.position, rot);
            Destroy(gameObject);
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > dataPackage.Range) {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}
