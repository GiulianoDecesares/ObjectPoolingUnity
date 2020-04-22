using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<ObjectType> where ObjectType : Component, IReusable
{
    private ObjectType original;
    private Queue<ObjectType> objects;

    private int objectCount;

    public ObjectPool(ObjectType original)
    {
        this.original = original;
        this.objects = new Queue<ObjectType>();

        this.objectCount = 0;
    }

    private ObjectType Create()
    {
        ObjectType instance = Object.Instantiate(this.original, Vector3.zero, Quaternion.identity);
        this.objects.Enqueue(instance);
        
        this.objectCount++;
        
        return instance;
    }

    public ObjectType Get()
    {
        return this.objects.Count > 0 ? this.objects.Dequeue() : this.Create();
    }

    public void Release(ObjectType toRelease)
    {
        toRelease.Recycle();
        this.objects.Enqueue(toRelease);
    }

    public int Size()
    {
        return this.objectCount;
    }
}
