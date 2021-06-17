using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    [SerializeField] private GameObject banana, bananas;
    [SerializeField] private Transform spawnPoint;
    
    void Start()
    {
        GameObject newBanana = null;

        if (Random.Range(0, 10) > 2)
        {
            newBanana = Instantiate(banana, spawnPoint.position, quaternion.identity);
        }
        else
        {
            newBanana = Instantiate(bananas, spawnPoint.position, quaternion.identity);
        }

        newBanana.transform.parent = transform;
    }
}
