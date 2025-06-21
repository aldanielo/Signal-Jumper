using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public enum PlatformType { Safe, Danger, Speed, Bonus }
    public GameObject platformPrefab; // Assign your platform prefab
    public float spawnInterval = 1.5f; // Time between platform spawns
    public float spawnYPosition = 10f; // Y position where platforms spawn
    public float platformSpeed = 2f; // Speed at which platforms move downwards
    private float spawnXPositionLeft = -12f;
    private float spawnXPositionRight = 8f;

    private void Start()
    {
        // Start spawning platforms
        StartCoroutine(SpawnPlatforms());
    }
 
    private IEnumerator SpawnPlatforms()
    {
        while (true)
        {
            SpawnPlatform();
            yield return new WaitForSeconds(spawnInterval);
        }
    }



    private void SpawnPlatform()
    {
        // Instantiate platform at the top of the screen
        Vector3 spawnPosition = new Vector3(Random.Range(spawnXPositionLeft, spawnXPositionRight), spawnYPosition, 0f);
        GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        // Randomly select platform type
        PlatformType platformType = (PlatformType)Random.Range(0, 3);

        // Assign platform type and behavior
        PlatformMover platformScript = platform.AddComponent<PlatformMover>();
        platformScript.platformType = platformType;
        platformScript.speed = platformSpeed;

        // Set platform color based on type
        Renderer platformRenderer = platform.GetComponent<Renderer>();
        switch (platformType)
        {
            case PlatformType.Safe:
                platformRenderer.material.color = Color.blue;
                break;
            case PlatformType.Danger:
                platformRenderer.material.color = Color.red;
                break;
            case PlatformType.Speed:
                platformRenderer.material.color = Color.green;
                break;
        }
    }
}
