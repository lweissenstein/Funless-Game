using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Bomb, FireRateBoost }
    public PowerUpType type;

    [Header("Settings")]
    public float fireRateBoostAmount = 0.2f;

    // redundant?
    /* 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivatePowerUp();

            Destroy(gameObject);
        }
    }
    */

    public void ActivatePowerUp()
    {
        Debug.Log($"ActivatePowerUp called for {type}");
        switch (type)
        {
            case PowerUpType.Bomb:
                DamageAllEnemies();
                break;

            case PowerUpType.FireRateBoost:
                PlayerShooting ps = FindAnyObjectByType<PlayerShooting>();
                if (ps != null)
                {
                    ps.permanentFireRateBoost += 0.05f;
                }
                break;

        }
    }

    public void DamageAllEnemies()
    {
        Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (Enemy e in enemies)
        {
            e.TakeDamage(5);
        }
    }
}
