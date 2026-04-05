using UnityEngine;

public class BambooSpawnerScript : MonoBehaviour
{
    public GameObject bambooPrefab;
    public float heightOffset = 7;

    [Header("Spawn timing")]
    [Tooltip("Seconds between spawns at initial bamboo speed (min of random range).")]
    [SerializeField] float minSpawnInterval = 5f;
    [Tooltip("Seconds between spawns at initial bamboo speed (max of random range).")]
    [SerializeField] float maxSpawnInterval = 12f;

    [Header("Difficulty")]
    [SerializeField] float initialBambooSpeed = 5f;
    [SerializeField] int bambooPerSpeedIncrease = 10;
    [SerializeField] float speedIncreaseAmount = 5f;

    float _timer;
    float _nextSpawnInterval;
    int _spawnCount;

    void Awake()
    {
        _spawnCount = 0;
        _timer = 0f;
        BambooMoveScript.ResetSharedMoveSpeed(initialBambooSpeed);
    }

    void Start()
    {
        SpawnBamboo();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _nextSpawnInterval)
            SpawnBamboo();
    }

    void ScheduleNextSpawn()
    {
        float speed = Mathf.Max(0.01f, BambooMoveScript.SharedMoveSpeed);
        float scale = initialBambooSpeed / speed;
        float minT = minSpawnInterval * scale;
        float maxT = maxSpawnInterval * scale;
        _nextSpawnInterval = Random.Range(minT, maxT);
    }

    void SpawnBamboo()
    {
        _spawnCount++;
        if (_spawnCount % bambooPerSpeedIncrease == 0)
            BambooMoveScript.AddSharedMoveSpeed(speedIncreaseAmount);

        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Instantiate(bambooPrefab, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);

        _timer = 0f;
        ScheduleNextSpawn();
    }
}
