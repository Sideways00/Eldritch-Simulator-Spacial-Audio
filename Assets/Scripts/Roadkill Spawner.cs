using UnityEngine;
using System.Collections.Generic;

public class RoadkillSpawner : MonoBehaviour
{
    [Header("Animal Prefabs")]
    public GameObject[] animalPrefabs;

    [Header("Spawn Settings")]
    public int numberOfAnimals = 30;
    public float roadLength = 500f;
    public float roadWidth = 8f;
    public float startDistance = 20f;
    public float animalHeight = 0.5f;

    [Header("Spacing")]
    public float minimumDistance = 10f;
    public int maxAttemptsPerAnimal = 50;

    void Start()
    {
        SpawnAnimals();
    }

    void SpawnAnimals()
    {
        if (animalPrefabs == null || animalPrefabs.Length == 0)
        {
            Debug.LogWarning("Assign animal prefabs!");
            return;
        }

        List<Vector3> spawnPositions = new List<Vector3>();

        for (int i = 0; i < numberOfAnimals; i++)
        {
            bool positionFound = false;

            for (int attempt = 0;
                 attempt < maxAttemptsPerAnimal;
                 attempt++)
            {
                float distance = Random.Range(startDistance, roadLength);
                float sideways = Random.Range(-roadWidth / 2f, roadWidth / 2f);

                Vector3 position = transform.position
                    + transform.forward * distance
                    + transform.right * sideways;

                position.y += animalHeight;

                bool tooClose = false;

                foreach (Vector3 existingPosition in spawnPositions)
                {
                    if (Vector3.Distance(
                        position, existingPosition) < minimumDistance)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                {
                    spawnPositions.Add(position);
                    positionFound = true;
                    break;
                }
            }

            if (!positionFound)
            {
                Debug.LogWarning(
                    "Could not find enough space for all animals.");
                continue;
            }

            GameObject animal = animalPrefabs[
                Random.Range(0, animalPrefabs.Length)];

            Instantiate(
                animal,
                spawnPositions[spawnPositions.Count - 1],
                Quaternion.Euler(0f, Random.Range(0f, 360f), 0f)
            );
        }
    }
}
