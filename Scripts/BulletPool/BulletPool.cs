using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour, IPoolable<Bullet>
{
    public Queue<Bullet> BulletQueue { get; private set; }
    
    [SerializeField]
    [Tooltip("Bullet prefab")]
    private GameObject _bulletPrefab;

    public int PoolSize { get; private set; }

    public void AddToPool(Bullet obj)
    {
        if (BulletQueue.Contains(obj)) return;
        if (BulletQueue.Count >= PoolSize || obj == null) return;        

        obj.gameObject.transform.position = transform.position;
        obj.gameObject.transform.SetParent(transform);
        obj.gameObject.SetActive(false);

        BulletQueue.Enqueue(obj);
    }

    public Bullet GetFromPool()
    {
        if (BulletQueue.Count <= 0) return null;

        var bullet = BulletQueue.Dequeue();
        bullet.gameObject.SetActive(true);
        bullet.gameObject.transform.SetParent(null, true);

        return bullet;
    }

    private void OnBulletDestroy(Bullet bullet)
    {
        AddToPool(bullet);
    }

    void Start()
    {
        Bullet bullet;

        PoolSize = 5;
        BulletQueue = new Queue<Bullet>(PoolSize);

        for(int i = 0; i < PoolSize; i++)
        {
            bullet = Instantiate(_bulletPrefab, gameObject.transform.localPosition,
                                  Quaternion.identity, gameObject.transform).GetComponent<Bullet>();
            bullet.OnBulletDestroy += OnBulletDestroy;

            AddToPool(bullet);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < BulletQueue.Count; i++)
            Destroy(BulletQueue.Dequeue());
    }


}
