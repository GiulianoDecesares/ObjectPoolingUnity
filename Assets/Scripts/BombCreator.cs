using UnityEngine;

public class BombCreator : IObjectCreator<Bomb>
{
    private Bomb bombPrefab;

    public BombCreator(Bomb bombPrefab)
    {
        this.bombPrefab = bombPrefab; }
    
    public Bomb Create()
    {
        return Object.Instantiate(this.bombPrefab, Vector3.zero, Quaternion.identity);
    }
}
