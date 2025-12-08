using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;

    [Header("PowerUps")]
    public float permanentFireRateBoost = 0f;

    private float fireTimer = 0f;
    private Vector2 shootDirection = Vector2.down;

    void Update()
    {
        fireTimer -= Time.deltaTime;

        shootDirection = GetMouseDirection();

        float effectiveFireRate = Mathf.Max(0.05f, fireRate - permanentFireRateBoost);

        if (fireTimer <= 0f)
        {
            Shoot();
            fireTimer = effectiveFireRate;
        }
    }

    Vector2 GetMouseDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        return (worldMousePos - transform.position).normalized;
    }

    void Shoot()
    {
        if (bulletPrefab == null) return;

        Transform activeChild = null;
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                activeChild = child;
                break;
            }
        }
        if (activeChild == null) return;
        
        Transform gunPivot = activeChild.Find("PlayerGunSprite");
        if (gunPivot == null) return;

        Transform spawnPoint = gunPivot.Find("BulletSpawnPoint");
        if (spawnPoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        Bullet b = bullet.GetComponent<Bullet>();

        if (b != null)
        {

            b.direction = shootDirection;

        }
    }
}