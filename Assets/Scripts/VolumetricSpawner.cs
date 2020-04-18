using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumetricSpawner : MonoBehaviour
{
    [SerializeField] private Collider spawnVolume;
    
    [Space]
    [SerializeField] private Bomb bombPrefab;
    [SerializeField] private float spawnInterval;

    private float timeSinceLastBomb;
    
    // Pooling improvements
    [SerializeField] private uint bombsAmount;
    
    private Bomb[] instantiatedBombs;

    private void Awake()
    {
        this.timeSinceLastBomb = 0;

        // Create and populate bombs 
        this.instantiatedBombs = new Bomb[this.bombsAmount];
        
        for (int index = 0; index < this.instantiatedBombs.Length; index++)
        {
            this.instantiatedBombs[index] = Instantiate(this.bombPrefab, this.transform.position, Quaternion.identity);
            this.instantiatedBombs[index]?.gameObject.SetActive(false); // Disable bomb 
        }
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

            // Get the bomb if its not active, give it a new position and activate it
            foreach (Bomb current in this.instantiatedBombs)
            {
                if (!current.isActiveAndEnabled)
                {
                    current.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
                    current.gameObject.SetActive(true);
                    break; // Break so we use only one bomb
                }
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
