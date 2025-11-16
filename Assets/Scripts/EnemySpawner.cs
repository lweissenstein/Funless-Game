using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject EnemyEasy;
    public GameObject EnemyMiddle;
    public GameObject EnemyHard;


    [Header("Spawn Settings")]
    public float spawnInterval = 2.0f;   // Interval of spawns
    public float spawnDistance = 1.0f;   // how far away from camera they spawn

    [Header("Difficulty Scaling")]
    public float difficultyIncreaseRate = 0.1f;
    private float difficulty = 0f;
    public float minimumSpawnInterval = 0.2f;
    public float spawnRateIncrease = 0.05f;


    private Camera cam;
    private float timer = 0f;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        timer += Time.deltaTime;
        difficulty += Time.deltaTime * difficultyIncreaseRate;

        // spawn Rate difficulty
        spawnInterval = Mathf.Max(
            minimumSpawnInterval, spawnInterval - (Time.deltaTime * spawnRateIncrease)
        );

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        GameObject prefab = ChooseEnemyByDifficulty();
        if (prefab == null) return;

        Vector2 spawnPos = GetSpawnPosition();
        Instantiate(prefab, spawnPos, Quaternion.identity);

    }

    GameObject ChooseEnemyByDifficulty()
    {
       // difficulty influences spawn of enemies

        float eEasy = Mathf.Clamp01(1.0f - difficulty * 0.05f);
        float eMiddle = Mathf.Clamp01(0.2f + difficulty * 0.03f);
        float eHard = Mathf.Clamp01(0.04f + difficulty * 0.015f);

        float total = eEasy + eMiddle + eHard;
        float rand = Random.value * total;

        if (rand < eEasy) return EnemyEasy;
        rand -= eEasy;

        if (rand < eMiddle) return EnemyMiddle;
        return EnemyHard;
    }
       
    Vector2 GetSpawnPosition()
    {
        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: return new Vector2(Random.Range(-width, width), height + spawnDistance);
            case 1: return new Vector2(Random.Range(-width, width), -height - spawnDistance);
            case 2: return new Vector2(-width - spawnDistance, Random.Range(-height, height));
            default: return new Vector2(width + spawnDistance, Random.Range(-height, height));
        }

    }
}
