using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameObjectPool
{
    private GameObject prefabReference;
    private List<GameObject> pool;
    
    public GameObjectPool(GameObject prefab)
    {
        this.prefabReference = prefab;
        this.pool = new List<GameObject>();
    }

    public GameObject GetGameObject()
    {
        // Search for a disable object in the list
        GameObject result = this.pool.FirstOrDefault(current => !current.activeInHierarchy);

        // If none, create one
        if (result == null)
        {
            result = Object.Instantiate(this.prefabReference, Vector3.zero, Quaternion.identity); // !!
            result.gameObject.SetActive(false); // !!
            this.pool.Add(result);
        }
        
        Debug.Log($"Game objects amount {this.pool.Count}");
        
        return result;
    }
}
