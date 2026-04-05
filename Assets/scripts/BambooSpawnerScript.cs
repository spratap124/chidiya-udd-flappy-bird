using UnityEngine;

public class BambooSpawnerScript : MonoBehaviour
{   
    public GameObject bambooPrefab;
    public float spawnRate;
    private float timer = 0;
    public float heightOffset = 7;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        SpawnBamboo();
    }

    // Update is called once per frame
    void Update()
    {   // spawn rate is random between 2 and 5
        spawnRate = Random.Range(5f, 12f);
        timer += Time.deltaTime;
        if (timer > spawnRate)
        {
            SpawnBamboo();
        }
    }

    void SpawnBamboo()
    {   
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Instantiate(bambooPrefab, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        timer = 0;
    }
}
