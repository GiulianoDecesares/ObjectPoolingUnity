using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private uint memoryConsumptionInBytes;
    [SerializeField] private float detonationTimeAfterContact;
    
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
        Destroy(this.gameObject, this.detonationTimeAfterContact);
    }
}
