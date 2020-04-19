using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour, IReusable
{
    [SerializeField] private uint memoryConsumptionInBytes;

    public event UnityAction<Bomb> OnUseFinished;
    
    private byte[] memoryConsumption;

    private void Start()
    {
        this.EatRam(this.memoryConsumptionInBytes);
    }

    private void EatRam(uint bytes)
    {
        if (this.memoryConsumption == null)
            this.memoryConsumption = new byte[bytes];
    }

    IEnumerator ReleaseCoroutine()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        this.OnUseFinished?.Invoke(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        StartCoroutine(this.ReleaseCoroutine());
    }

    public void Recycle()
    {
        this.gameObject.SetActive(false);
    }
}
