using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Game Over!");
            UIManager.Instance.GameOver();
        }

        if (other.CompareTag("PowerUp"))
        {
            Debug.Log("Player picked up a PowerUp!");

            PowerUp pu = other.GetComponent<PowerUp>();
            if (pu != null)
            {
                pu.ActivatePowerUp();
                Destroy(other.gameObject);
            }
        }
    }
}
