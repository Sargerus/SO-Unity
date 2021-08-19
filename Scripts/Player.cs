using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BulletPool _bulletPool;
    private float _modelExtentY;
    private float _speed = 5.0f;

    private bool isReloading = false;

    void Start()
    {
        if (!_bulletPool)
            _bulletPool = GetComponentInChildren<BulletPool>();

        _modelExtentY = GetComponent<MeshRenderer>().bounds.extents.y;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        Vector3 _cameraBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 10f));
        Vector3 _cameraBottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 10f));

        transform.position = new Vector3(Mathf.Clamp(transform.localPosition.x + Input.GetAxis("Horizontal") * Time.fixedDeltaTime * _speed, _cameraBottomLeft.x, _cameraBottomRight.x), transform.localPosition.y,0);        

    }

    private void Shoot()
    {
        if (!_bulletPool || isReloading) return;

        var bullet = _bulletPool.GetFromPool();

        if (bullet != null)
            bullet.transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y + _modelExtentY * 2, transform.position.z);

        StartCoroutine(Reloading());
    }

    private IEnumerator Reloading()
    {
        isReloading = true;

        yield return new WaitForSeconds(0.3f);

        isReloading = false;
    }
}
