using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1; // 1 hit kill 

    public void TakeDamage(int damage = 1)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject); // destroy bullet on hit
        }
    }
}
