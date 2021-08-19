using UnityEngine;

public class Obstacle : AbstractObstacle
{
    public Block data;

    void Start()
    {
        if (!_spriteRenderer)
        {
            var colliderBorders = GetComponent<BoxCollider2D>().bounds;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            var texture2D = ScaleTexture(data.sprite.texture, (int)((colliderBorders.max.x - colliderBorders.min.x) * data.sprite.pixelsPerUnit), (int)((colliderBorders.max.y - colliderBorders.min.y) * data.sprite.pixelsPerUnit));
            _spriteRenderer.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
        }

        _health = data.health;        
    }

    public override void GetDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Destroy(gameObject);
    }
}
