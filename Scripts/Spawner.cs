using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject fruitPrefab;
    public  float spawnInterval = 10.0f;
    private float spawnTimer;
    float minXSpawn;
    float maxXSpawn;
    float minYSpawn;
    float maxYSpawn;
    GameObject currentFruit;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnInterval;
        var wallBehaviours = FindObjectsOfType<WallBehaviour>();
        foreach (var wallBehaviour in wallBehaviours)
        {
            CapsuleCollider2D[] capsuleColliders = wallBehaviour.GetComponentsInChildren<CapsuleCollider2D>();

            var firstCapsule = true;
            foreach (var capsuleCollider in capsuleColliders)
            {
                var bounds = capsuleCollider.bounds.center;

                if (firstCapsule)
                {
                    minXSpawn = bounds.x;
                    maxXSpawn = bounds.x;
                    minYSpawn = bounds.y;
                    maxYSpawn = bounds.y;
                    firstCapsule = false;
                }

                if (bounds.x < minXSpawn)
                {
                    minXSpawn = bounds.x;
                }
                else if (bounds.x > maxXSpawn)
                {
                    maxXSpawn = bounds.x;
                }

                if (bounds.y < minYSpawn)
                {
                    minYSpawn = bounds.y;
                }
                else if (bounds.y > maxYSpawn)
                {
                    maxYSpawn = bounds.y;
                }

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        // Check if the spawn timer exceeds the spawn interval
        if (spawnTimer >= spawnInterval)
        {
            // Reset the timer
            spawnTimer = 0.0f;

            // Spawn the prefab
            for (int i = 0; i < 20; i++)
            {

                var spawnPosition = new Vector3(UnityEngine.Random.Range(minXSpawn, maxXSpawn), UnityEngine.Random.Range(minYSpawn, maxYSpawn), 0);
                var fruitCollider = fruitPrefab.GetComponent<CircleCollider2D>();

                if (Physics2D.OverlapBox(spawnPosition, new Vector2(fruitCollider.radius, fruitCollider.radius), 0f))
                {
                    continue;
                }

                currentFruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
                Destroy(currentFruit, spawnInterval);
                return;
            }
        }
    }
}
