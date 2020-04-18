using System.Collections.Generic;

public class ObjectPool<Type> where Type : IReusable
{
    private readonly IObjectCreator<Type> objectCreator;
    private readonly Queue<Type> freeObjects;
    
    public ObjectPool(IObjectCreator<Type> objectCreator)
    {
        this.objectCreator = objectCreator;
        this.freeObjects = new Queue<Type>();
    }

    public Type GetGameObject()
    {
        if (this.freeObjects.Count < 1)
        {
            IReusable reusable = this.objectCreator.Create();
            reusable.OnReleased += this.OnObjectReleased;
            
            this.freeObjects.Enqueue((Type)reusable);
        }
        
        return this.freeObjects.Dequeue();
    }
    
    private void OnObjectReleased(IReusable reusable)
    {
        if (reusable != null)
        {
            this.freeObjects.Enqueue((Type)reusable);
        }
    }
}
