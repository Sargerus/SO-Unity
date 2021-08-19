using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private BoxCollider2D _boxCollider2D;

    public Action<Bullet> OnBulletDestroy;

    private void Start()
    {
        if(!_boxCollider2D)
            _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        if(_boxCollider2D)
            _boxCollider2D.enabled = true;
    }

    void Update()
    {
        transform.Translate(transform.up * _speed * Time.deltaTime, Space.World);

        var topY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 10f)).y;
        
        if (transform.localPosition.y >= topY)
            OnBulletDestroy(this);
    }

    //private void OnBecameInvisible()
    //{
    //    OnBulletDestroy(this);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AbstractObstacle obstacle;

        if(_boxCollider2D)
            _boxCollider2D.enabled = false;

        if (obstacle = collision.GetComponent<AbstractObstacle>())
        {
            obstacle.GetDamage(1);
            OnBulletDestroy(this);
        }
    }
}
