using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Game Over!");

            // Score speichern
            ScoreManager sm = FindAnyObjectByType<ScoreManager>();
            if (sm != null)
                sm.SaveScoreToFile();

            // Game Over Screen
            UIManager.Instance.GameOver();
        }
    }
}
