using System;
using UnityEngine;

public class Bomb : MonoBehaviour, IReusable
{
    [SerializeField] private uint memoryConsumptionInBytes;
    
    public event Action<IReusable> OnReleased;
    
    private byte[] memoryConsumption;

    private void OnEnable()
    {
        this.EatRam(this.memoryConsumptionInBytes);
    }

    private void EatRam(uint bytes)
    {
        this.memoryConsumption = new byte[bytes];

        for (int index = 0; index < this.memoryConsumption.Length; index++)
        {
            this.memoryConsumption[index] = 0;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Disable bomb after 1.5 seconds when it collides whit the flor
        Invoke(nameof(this.Disable), 1.5f);
    }

    private void Disable()
    {
        this.OnReleased?.Invoke(this);
        this.gameObject.SetActive(false);
    }
}
