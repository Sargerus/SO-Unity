using UnityEngine;

public class AnimatedObstacle : AbstractObstacle
{
    public AnimatedBlock data;
    private Sprite[] _healthAnimations;

    void Start()
    {
        _healthAnimations = data.health; //when accessing array in object c# copy this array, to prevent it put array into local variable
        _health = _healthAnimations.Length;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        if (_health < 0 || _healthAnimations.Length <= 0) return;
        if (!_spriteRenderer) return;

        var colliderBorders = GetComponent<BoxCollider2D>().bounds;
        var pixelPerUnit = _healthAnimations[_healthAnimations.Length - _health].pixelsPerUnit;

        var texture2D = ScaleTexture(_healthAnimations[_healthAnimations.Length -_health].texture, 
                                    (int)((colliderBorders.max.x - colliderBorders.min.x) * pixelPerUnit), 
                                    (int)((colliderBorders.max.y - colliderBorders.min.y) * pixelPerUnit));
        _spriteRenderer.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
    }

    public override void GetDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Destroy(gameObject);
        else ChangeSprite();
    }
}
