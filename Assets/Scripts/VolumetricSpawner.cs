using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VolumetricSpawner : MonoBehaviour
{
    [SerializeField] private Collider spawnVolume;
    [SerializeField] private float spawnInterval;

    [SerializeField] private Bomb bombPrefab;

    private float timeSinceLastBomb;

    private ObjectPool<Bomb> bombPool;

    private void Awake()
    {
        this.timeSinceLastBomb = 0;
        this.bombPool = new ObjectPool<Bomb>(new BombCreator(this.bombPrefab));
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
            Bomb bomb = this.bombPool.GetGameObject();
            
            if (bomb != null)
            {
                bomb.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
                bomb.gameObject.SetActive(true);
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
