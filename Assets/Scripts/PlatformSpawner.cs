using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner instance;

    [SerializeField] private GameObject leftPlatform, rightPlatform;
    [SerializeField] private GameObject bird;
    [SerializeField] private Transform platformParent;
    [SerializeField] private float leftXMin, leftXMax;
    [SerializeField] private float rightXMin, rightXMax;
    [SerializeField] private float yThreshold;
    [SerializeField] private int platformsPerSpawn = 8;
    [SerializeField] private float birdXMin = -2.3f, birdXMax = 2.3f;
    [SerializeField] private float birdY = 5f;

    private float lastY;
    private int platformSpawned;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        lastY = transform.position.y;
        SpawnPlatforms();
    }

    public void SpawnPlatforms()
    {
        Vector2 temp = transform.position;
        GameObject newPlatform = null;

        for (int i = 0; i < platformsPerSpawn; i++)
        {
            temp.y = lastY;

            if ((platformSpawned % 2) == 0)
            {
                temp.x = Random.Range(leftXMin, leftXMax);
                newPlatform = Instantiate(rightPlatform, temp, quaternion.identity);
            }
            else
            {
                temp.x = Random.Range(rightXMin, rightXMax);
                newPlatform = Instantiate(leftPlatform, temp, quaternion.identity);
            }

            newPlatform.transform.parent = platformParent;
            lastY += yThreshold;
            platformSpawned++;
        }

        if (Random.Range(0, 2) == 0)
        {
            SpawnBird();
        }
    }

    private void SpawnBird()
    {
        Vector2 temp = transform.position;
        temp.x = Random.Range(birdXMin, birdXMax);
        temp.y += birdY;

        var newBird = Instantiate(bird, temp, quaternion.identity);
        newBird.transform.parent = platformParent;
    }
}
