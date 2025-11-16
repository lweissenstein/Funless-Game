using UnityEngine;
// using UnityEngine.SceneManagement; // Later for Game Over or Reload Screen

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
         
            Debug.Log("Game Over!");

            UIManager.Instance.GameOver();

            Time.timeScale = 0f;
        }
    }
}
