using System.Collections.Generic;

public interface IPoolable<T> where T : Bullet
{
    Queue<T> BulletQueue { get; }

    int PoolSize { get; }

    void AddToPool(T obj);

    T GetFromPool();
}
