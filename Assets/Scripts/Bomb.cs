using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private uint memoryConsumptionInBytes = 10;
    private int[] memoryConsumption;

    private void OnEnable()
    {
        this.EatRam(this.memoryConsumptionInBytes);
    }

    private void EatRam(uint bytes)
    {
        this.memoryConsumption = new int[bytes];

        for (int index = 0; index < this.memoryConsumption.Length; index++)
            this.memoryConsumption[index] = index;
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
}
