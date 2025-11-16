using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int health = 1; // set health how many bullets they take
    public float moveSpeed = 2f;

    private Transform player; // reference to player postiion

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {   
        // Move towards player
        if (player != null)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
        }
    }

    // Damage System

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
        FindAnyObjectByType<ScoreManager>().AddScore(100);

        Destroy(gameObject);
    }


    // Enemy hit by Bullet

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject); // destroy bullet on hit
        }
    }
}
