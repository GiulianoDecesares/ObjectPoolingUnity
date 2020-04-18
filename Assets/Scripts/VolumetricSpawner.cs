using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VolumetricSpawner : MonoBehaviour
{
    [SerializeField] private Collider spawnVolume;
    
    [Space]
    [SerializeField] private Bomb bombPrefab;
    [SerializeField] private float spawnInterval;

    private float timeSinceLastBomb;

    private GameObjectPool bombPool;

    private void Awake()
    {
        this.timeSinceLastBomb = 0;
        this.bombPool = new GameObjectPool(this.bombPrefab.gameObject);
    }

    private void Update()
    {
        if (Time.time - this.timeSinceLastBomb > this.spawnInterval)
        {
            this.SpawnBomb();
        }
    }

    private void SpawnBomb()
    {
        this.timeSinceLastBomb = Time.time;
        
        // Take random position in spawn volume
        Bounds? targetBounds = spawnVolume != null ? spawnVolume.bounds : (Bounds?) null;

        if (targetBounds != null)
        {
            Vector3 spawnPosition = this.RandomPointInBounds(targetBounds.Value);
            GameObject bomb = this.bombPool.GetGameObject();
            
            if (bomb != null)
            {
                bomb.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
                bomb.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Null target bounds");
        }
    }
    
    private Vector3 RandomPointInBounds(Bounds bounds) {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }

}
