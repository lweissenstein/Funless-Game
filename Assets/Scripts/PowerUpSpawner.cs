using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("PowerUp Prefabs")]
    public GameObject bombPowerUpPrefab;
    public GameObject fireRatePowerUpPrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 8f;       
    public float spawnRandomOffset = 2f;  

    private Camera cam;
    private float timer;

    void Start()
    {
        cam = Camera.main;
        ResetTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnPowerUp();
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        timer = spawnInterval + Random.Range(-spawnRandomOffset, spawnRandomOffset);
        timer = Mathf.Max(1f, timer); 
    }

    void SpawnPowerUp()
    {
        GameObject prefabToSpawn;

        // 50 / 50 chance
        if (Random.value < 0.5f)
            prefabToSpawn = bombPowerUpPrefab;
        else
            prefabToSpawn = fireRatePowerUpPrefab;

        Vector2 pos = GetRandomScreenPosition();
        Instantiate(prefabToSpawn, pos, Quaternion.identity);
    }

    Vector2 GetRandomScreenPosition()
    {
        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        return new Vector2(
            Random.Range(-width * 0.9f, width * 0.9f),
            Random.Range(-height * 0.9f, height * 0.9f)
        );
    }
}
