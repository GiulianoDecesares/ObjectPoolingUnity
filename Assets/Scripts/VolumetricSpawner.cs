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
    
    private List<Bomb> instantiatedBombs; // Pool 

    private void Awake()
    {
        this.timeSinceLastBomb = 0;

        // Create and populate bombs 
        this.instantiatedBombs = new List<Bomb>(); // Pool
    }

    private void Update()
    {
        if (Time.time - this.timeSinceLastBomb > this.spawnInterval)
        {
            Debug.Log($"Bombs amount is {this.instantiatedBombs.Count}");
            
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
            this.InstantiateBomb(spawnPosition);
        }
        else
        {
            Debug.LogError("Null target bounds");
        }
    }

    // Pool
    private void InstantiateBomb(Vector3 position)
    {
        // Get the bomb if its not active, give it a new position and activate it
        Bomb inactiveBomb = this.instantiatedBombs.FirstOrDefault(current => !current.isActiveAndEnabled);

        if (inactiveBomb == null)
        {
            inactiveBomb = Instantiate(this.bombPrefab, position, Quaternion.identity);
            inactiveBomb.gameObject.SetActive(false);
            this.instantiatedBombs.Add(inactiveBomb);
        }
        
        inactiveBomb.transform.SetPositionAndRotation(position, Quaternion.identity);
        inactiveBomb.gameObject.SetActive(true);
    }
    
    private Vector3 RandomPointInBounds(Bounds bounds) {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }

}
