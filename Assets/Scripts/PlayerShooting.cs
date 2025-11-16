using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;

    [Header("PowerUps")]
    public float permanentFireRateBoost = 0f; 

    private float fireTimer = 0f;
    private Vector2 lastDirection = Vector2.down;

    void Update()
    {
        fireTimer -= Time.deltaTime;

        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (move.magnitude > 0.1f)
            lastDirection = move.normalized;

        float effectiveFireRate = Mathf.Max(0.05f, fireRate - permanentFireRateBoost);

        if (fireTimer <= 0f)
        {
            Shoot();
            fireTimer = effectiveFireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || lastDirection == Vector2.zero) return;

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

        Transform spawnPoint = activeChild.Find("BulletSpawnPoint");
        if (spawnPoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
            b.direction = lastDirection.normalized;
    }
}
