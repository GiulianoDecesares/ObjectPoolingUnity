using UnityEngine;

public class VolumetricSpawner : MonoBehaviour
{
    [SerializeField] private Collider spawnVolume;
    [SerializeField] private float spawnInterval;
    [SerializeField] private Bomb bombPrefab;

    [SerializeField] private AnimationCurve plot;

    private float timeSinceLastBomb;
    private ObjectPool<Bomb> bombPool;

    private void Awake()
    {
        this.timeSinceLastBomb = 0;
        this.bombPool = new ObjectPool<Bomb>(this.bombPrefab);
        this.plot = new AnimationCurve();
    }

    private void Update()
    {
        if (Time.time - this.timeSinceLastBomb > this.spawnInterval)
        {
            this.SpawnBomb();
        }
        
        // Debug objects amount
        this.plot.AddKey(Time.realtimeSinceStartup, this.bombPool.Size());
    }

    private Vector3 GetBombRandomPosition()
    {
        Vector3 result = Vector3.zero;
        
        // Get bounds
        Bounds? bounds = spawnVolume != null ? spawnVolume.bounds : (Bounds?) null;

        if (bounds.HasValue)
        {
            // Generate random position
            result = new Vector3(
                Random.Range(bounds.Value.min.x, bounds.Value.max.x),
                Random.Range(bounds.Value.min.y, bounds.Value.max.y),
                Random.Range(bounds.Value.min.z, bounds.Value.max.z)
            );
        }
        else
        {
            Debug.LogError("Null bounds");
        }

        return result;
    }

    private void OnBombDisposed(Bomb bomb)
    {
        bomb.OnUseFinished -= this.OnBombDisposed;
        this.bombPool.Release(bomb);
    }

    private void SpawnBomb()
    {
        // Catch time
        this.timeSinceLastBomb = Time.time;
        
        // Get bomb from pool
        Bomb bomb = this.bombPool.Get();

        // Listen to bomb when released
        bomb.OnUseFinished += this.OnBombDisposed;

        if (bomb != null)
        {
            // Set new position and activate bomb
            bomb.transform.SetPositionAndRotation(this.GetBombRandomPosition(), Quaternion.identity);
            bomb.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Null bomb");
        }
    }
}
